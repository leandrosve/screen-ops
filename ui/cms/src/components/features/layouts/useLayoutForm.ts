import { LayoutCreateDto, LayoutElementType } from "@/model/layout/Layout";
import { useCallback, useMemo, useState } from "react";

export interface LayoutConfig {
  rows: number;
  columns: number;
}

export interface BasicLayoutElement {
  key: string;
  type: LayoutElementType;
  label: string;
}

export interface BasicLayout {
  elements: BasicLayoutElement[];
  rows: number;
  columns: number;
  totalSeats?: number; //Solo para mostar en la UI, pero lo pongo aca asi evito iterar mas veces
}

const SEAT_TYPES = new Set([
  LayoutElementType.STANDARD,
  LayoutElementType.VIP,
  LayoutElementType.ACCESSIBLE,
  LayoutElementType.DISABLED,
]);

const mapToBasicLayout = (
  layout: LayoutCreateDto
): BasicLayout  => {
  return {
    rows: layout.rows,
    columns: layout.columns,
    elements: layout.elements.map((e) => ({
      label: e.label,
      key: `${e.index}`,
      type: e.type,
    })),
    totalSeats: layout.elements.reduce((total, element) => {
      return total + (SEAT_TYPES.has(element.type) ? 1 : 0);
    }, 0),
  };
};

// No es inmutable
const recalculateLabelsAndKeys = (layout: BasicLayout) => {
  let totalSeats = 0;
  for (let row = 0; row < layout.rows; row++) {
    let seatNumber = 1;
    const letter = String.fromCharCode(65 + row);
    for (let col = 0; col < layout.columns; col++) {
      let element = layout.elements[row * layout.columns + col];
      if (SEAT_TYPES.has(element.type)) {
        totalSeats++;
        element.label = `${letter}${seatNumber}`;
        seatNumber++;
      } else {
        element.label = "";
      }
      element.key = `${row}-${col}`;
    }
  }
  layout.totalSeats = totalSeats;
};

// Inicializa el layout con asientos estándar
const createLayout = (rows: number, columns: number): BasicLayout => {
  const elements: BasicLayoutElement[] = [];

  for (let row = 0; row < rows; row++) {
    for (let col = 0; col < columns; col++) {
      elements.push(createSeat(row, col, LayoutElementType.STANDARD));
    }
  }

  return {
    elements,
    rows,
    columns,
    totalSeats: elements.length,
  };
};

const resizeLayout = (
  currentLayout: BasicLayout,
  newRows?: number | null,
  newColumns?: number | null
): BasicLayout => {
  const elements: BasicLayoutElement[] = [];
  const previousColumns = currentLayout.columns;

  const rows = newRows ?? currentLayout.rows;
  const columns = newColumns ?? currentLayout.columns;
  for (let row = 0; row < rows; row++) {
    for (let col = 0; col < columns; col++) {
      // Calcular posición en el vector antiguo
      const oldPos = row * previousColumns + col;
      const existsInOld = row < currentLayout.rows && col < previousColumns;

      if (existsInOld) {
        // Mantener el elemento existente con posición actualizada
        elements.push({
          ...currentLayout.elements[oldPos],
        });
      } else {
        // Crear nuevo elemento
        elements.push(createSeat(row, col, LayoutElementType.STANDARD));
      }
    }
  }

  let newLayout: BasicLayout = {
    elements: elements,
    rows: rows,
    columns: columns,
  };

  recalculateLabelsAndKeys(newLayout);
  return newLayout;
};

// Factory function para crear asientos
const createSeat = (
  row: number,
  col: number,
  type: LayoutElementType
): BasicLayoutElement => ({
  key: `${row}-${col}`,
  label: `${String.fromCharCode(65 + row)}${col + 1}`,
  type: type,
});

function insertRow(
  layout: BasicLayout,
  position: "start" | "end"
): BasicLayout {
  const newElements: BasicLayoutElement[] = [];
  // Primero agregar la nueva fila
  for (let col = 0; col < layout.columns; col++) {
    newElements.push(createSeat(0, col, LayoutElementType.STANDARD));
  }

  if (position == "start") {
    newElements.push(...layout.elements);
  } else {
    newElements.unshift(...layout.elements);
  }

  const res = {
    elements: newElements,
    rows: layout.rows + 1,
    columns: layout.columns,
  };

  recalculateLabelsAndKeys(res);
  return res;
}

function insertColumn(
  layout: BasicLayout,
  position: "start" | "end"
): BasicLayout {
  let oldElements = layout.elements;
  const newElements: BasicLayoutElement[] = [];
  let oldIndex = 0;
  for (let row = 0; row < layout.rows; row++) {
    for (let col = 0; col <= layout.columns; col++) {
      if (position == "end" && col == layout.columns) {
        newElements.push(createSeat(row, col, LayoutElementType.STANDARD));
        continue;
      }
      if (position == "start" && col == 0) {
        newElements.push(createSeat(row, col, LayoutElementType.STANDARD));
        continue;
      }
      newElements.push(oldElements[oldIndex]);
      oldIndex++;
    }
  }

  const res = {
    elements: newElements,
    rows: layout.rows,
    columns: layout.columns + 1,
  };

  recalculateLabelsAndKeys(res);
  return res;
}

const useLayoutEditor = (initialLayout?: LayoutCreateDto | null) => {
  const [layout, setLayout] = useState<BasicLayout>(
    initialLayout ? mapToBasicLayout(initialLayout) : createLayout(10, 15)
  );

  const [selectedElementKeys, setSelectedElementKeys] = useState<string[]>([]);

  const findElementsByKey = useCallback(
    (keys: string[]): BasicLayoutElement[] => {
      const keySet = new Set(keys);
      return layout.elements.filter((e) => keySet.has(e.key));
    },
    [layout]
  );

  const selectedElements = useMemo(() => {
    return findElementsByKey(selectedElementKeys);
  }, [selectedElementKeys, layout]);

  // Cambiar el tipo de un elemento del layout
  const handleTypeChange = useCallback(
    (elements: BasicLayoutElement[], newType: LayoutElementType) => {
      const elementsKeySet = new Set(elements.map((e) => e.key));

      setLayout((prev) => {
        const next = { ...prev };

        next.elements.forEach((e) => {
          if (elementsKeySet.has(e.key)) {
            e.type = newType;
          }
        });

        recalculateLabelsAndKeys(next);
        return next;
      });
    },
    []
  );

  // Seleccionar un elemento
  const handleSelectElement = useCallback(
    (element: BasicLayoutElement, multiple?: boolean) => {
      setSelectedElementKeys((prev) =>
        multiple ? [...prev, element.key] : [element.key]
      );
    },
    []
  );

  const handleDeselectAll = useCallback(() => {
    setSelectedElementKeys([]);
  }, []);

  const handleInsertRow = useCallback((position: "start" | "end") => {
    setLayout((prev) => insertRow(prev, position));
  }, []);

  const handleInsertColumn = useCallback((position: "start" | "end") => {
    setLayout((prev) => insertColumn(prev, position));
  }, []);

  const handleResize = useCallback(
    (newRows?: number | null, newCols?: number | null) => {
      if (!newRows && !newCols) return;
      setLayout((prev) => resizeLayout(prev, newRows, newCols));
    },
    []
  );

  return {
    layout,
    selectedElements,
    selectedElementKeys,
    handleDeselectAll,
    handleTypeChange,
    handleSelectElement,
    handleResize,
    handleInsertColumn,
    handleInsertRow,
  };
};

export default useLayoutEditor;
