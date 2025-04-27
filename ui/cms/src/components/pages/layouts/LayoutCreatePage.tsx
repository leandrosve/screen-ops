import LayoutForm from "@/components/features/layouts/LayoutForm";
import { toaster } from "@/components/ui/toaster";
import { LayoutCreateDto } from "@/model/layout/Layout";
import { CmsRoutes } from "@/router/routes";
import LayoutService from "@/services/api/LayoutService";
import { LayoutErrors } from "@/validation/api-errors/LayoutErrors";
import { useCallback, useState } from "react";
import { useNavigate } from "react-router-dom";

const LayoutCreatePage = () => {
  const [error, setError] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();
  const submitAsync = useCallback(async (data: LayoutCreateDto) => {
    setLoading(true);
    const res = await LayoutService.create(data);
    setLoading(false);
    if (res.hasError) {
      setError(
        LayoutErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente."
      );
    }

    if (res.data) {
      toaster.create({
        title: "Se ha creado el layout",
        type: "success",
        duration: 1500,
      });

      navigate(CmsRoutes.LAYOUT_DETAIL.replace(":id", res.data?.id ?? ""));
    }
  }, []);
  return (

    <LayoutForm error={error} isSubmitting={loading} onSubmit={submitAsync} title="Añadir un Layout"/>
  );
};

export default LayoutCreatePage;
