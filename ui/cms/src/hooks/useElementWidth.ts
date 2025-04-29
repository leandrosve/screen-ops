import { useState, useEffect, RefObject } from "react";

export function useElementWidth<T extends HTMLElement | null>(
  ref: RefObject<T>,
  defaultWidth: number
) {
  const [width, setWidth] = useState(defaultWidth);

  useEffect(() => {
    if (!ref) return;
    if (!ref.current) return;

    const updateWidth = () => {
      setWidth(ref.current?.offsetWidth ?? defaultWidth);
    };

    updateWidth(); // set initial width

    const resizeObserver = new ResizeObserver(() => updateWidth());
    resizeObserver.observe(ref.current);

    return () => resizeObserver.disconnect();
  }, [ref]);

  return width;
}
