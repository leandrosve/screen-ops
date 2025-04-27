import Alert from "@/components/common/Alert";
import PageLoader from "@/components/common/PageLoader";
import LayoutForm from "@/components/features/layouts/LayoutForm";
import { toaster } from "@/components/ui/toaster";
import useEntityDetail from "@/hooks/useEntityDetail";
import { Layout, LayoutCreateDto } from "@/model/layout/Layout";
import { CmsRoutes } from "@/router/routes";
import LayoutService from "@/services/api/LayoutService";
import { LayoutErrors } from "@/validation/api-errors/LayoutErrors";
import { useCallback, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

const LayoutUpdatePage = () => {
  const {
    entity: layout,
    loading,
    error,
  } = useEntityDetail<Layout>({
    fetchFunction: (id: string) => LayoutService.getDetail(id),
  });

  if (loading) return <PageLoader />;

  if (error || !layout)
    return (
      <Alert
        width="full"
        status="error"
        autoFocus
        description={error ?? "Lo sentimos, no se ha encontrado la película"}
      />
    );

  if (!loading && layout) {
    return <LayoutUpdateForm layout={layout} />;
  }
  return <></>;
};

const mapLayoutToLayoutCreateDto = (layout: Layout): LayoutCreateDto => {
  console.log({layout})
  return {
    rows: layout.rows,
    columns: layout.columns,
    name: layout.name,
    elements: layout.elements.map((e) => ({
      label: e.label,
      type: e.type,
      index: e.index,
    })),
  };
};

function getModifiedFields(
  initial: LayoutCreateDto,
  data: LayoutCreateDto
): Partial<LayoutCreateDto> {
  const result: Partial<LayoutCreateDto> = {};

  // Compare simple fields
  if (initial.name !== data.name) {
    result.name = data.name;
  }
  if (initial.rows !== data.rows) {
    result.rows = data.rows;
  }
  if (initial.columns !== data.columns) {
    result.columns = data.columns;
  }

  if (initial.elements.length !== data.elements.length) {
    // If lengths are different, consider all elements as changed
    result.elements = data.elements;
    return result;
  }

  // Compare each element
  let hasModifiedElements = false;

  for (let i = 0; i < initial.elements.length; i++) {
    const initialElement = initial.elements[i];
    const currentElement = data.elements[i];
    if (
      initialElement.label !== currentElement.label ||
      initialElement.index !== currentElement.index ||
      initialElement.type !== currentElement.type
    ) {
      hasModifiedElements = true;
      break;
    }
  }

  if (hasModifiedElements) {
    result.elements = data.elements;
  }

  return result;
}
const LayoutUpdateForm = ({ layout }: { layout: Layout }) => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const initialValues = useMemo(
    () => mapLayoutToLayoutCreateDto(layout),
    [layout]
  );
  const navigate = useNavigate();
  const submitAsync = useCallback(
    async (data: LayoutCreateDto) => {
      setLoading(true);

      const modifiedFields = getModifiedFields(initialValues, data);
      const res = await LayoutService.update(layout.id, modifiedFields);
      setLoading(false);
      if (res.hasError) {
        setError(
          LayoutErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
        );
      }

      if (res.data) {
        toaster.create({
          title: "Se han guardado los cambios",
          type: "success",
          duration: 1500,
        });

        navigate(CmsRoutes.LAYOUT_DETAIL.replace(":id", res.data?.id ?? ""));
      }
    },
    [layout.id]
  );

  return (
    <LayoutForm
      error={error}
      initialValues={initialValues}
      isSubmitting={loading}
      onSubmit={submitAsync}
      title="Editar Layout"
    />
  );
};

export default LayoutUpdatePage;
