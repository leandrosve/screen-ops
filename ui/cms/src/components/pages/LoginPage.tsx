import {
  Box,
  Button,
  Card,
  Field,
  Flex,
  Heading,
  Input,
  Stack,
  Text,
} from "@chakra-ui/react";
import { Link, useSearchParams } from "react-router-dom";
import { useMemo, useState, useEffect } from "react";
import AuthService from "../../services/api/AuthService";
import SessionService from "../../services/SessionService";
import { Tooltip } from "@/components/common/Tooltip";
import { CmsRoutes } from "@/router/routes";
import Alert from "@/components/common/Alert";
import { LoginErrors } from "@/validation/api-errors/LoginErrors";

const LoginPage = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const [submiting, setSubmiting] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const enableSubmit = useMemo(() => !!(email && password), [email, password]);

  const [searchParams, setSearchParams] = useSearchParams();

  const onSubmit = async (e: React.FormEvent) => {
    setError(null);
    setSuccess(null);
    e.preventDefault();
    setSubmiting(true);
    const res = await AuthService.login({ email, password });
    if (res.hasError) {
      setSubmiting(false);
      setError(LoginErrors[res.error] ?? "Algo salió mal! Intentalo nuevamente.");
      return;
    }
    SessionService.saveLocalSession(res.data);
    location.replace("/");
  };

  useEffect(() => {
    if (searchParams.get("tokenExpired")) {
      setError("La sesión ha expirado, por favor vuelve a iniciar sesión.");
    }
    if (searchParams.get("logout")) {
      setSuccess("Se ha cerrado la sesión. Hasta pronto!");
    }
    setSearchParams("", { replace: true });
  }, []);

  return (
    <Flex
      grow={1}
      align="center"
      justify="space-between"
      direction={"column"}
    >
      <Card.Root
        p="6"
        background="bg.300"
        rounded={40}
        display="flex"
        mt={10}
        flexDirection={"column"}
      >
        <Card.Body gap="2">
          <Flex>
            <form onSubmit={onSubmit}>
              <Stack align="stretch" width={["auto", 350]}>
                 <Flex alignSelf='center' alignItems="flex-end" gap={3} position='relative'>
                    <Heading
                      as="span"
                      fontWeight="light"
                      fontSize={30}
                      color="brand.400"
                    >
                      ScreenOps
                    </Heading>
                    <Box as='span' fontWeight="bold" fontSize='md' position='absolute' top={0} left={"100%"}
                      color="brand.500">CMS</Box>
                  </Flex>
                <Heading mt={5} textAlign="center" as="legend">
                  Iniciar Sesión
                </Heading>
                <Alert status="error" autoFocus description={error} />
                <Alert status="info" autoFocus description={success} />
                <Field.Root>
                  <Field.Label mt={2}>Email</Field.Label>
                  <Input
                    type="email"
                    size={"sm"}
                    placeholder="Email"
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </Field.Root>
                <Field.Root>
                  <Field.Label mt={2}>Contraseña</Field.Label>
                  <Input
                    type="password"
                    placeholder="Contraseña"
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </Field.Root>

                <Tooltip
                  content="Por favor completa los campos necesarios"
                  disabled={enableSubmit}
                  showArrow
                  aria-label="A tooltip"
                >
                  <Button
                    colorPalette="brand"
                    width="100%"
                    mt={5}
                    type="submit"
                    disabled={!enableSubmit}
                    loading={submiting}
                  >
                    Continuar
                  </Button>
                </Tooltip>

                <Text textAlign={"center"} mt={2}>
                  ¿No estas registrado?{" "}
                  <Link to={CmsRoutes.SIGNUP}>
                    <b>Registrarme</b>
                  </Link>
                </Text>
              </Stack>
            </form>
          </Flex>
        </Card.Body>
      </Card.Root>
    </Flex>
  );
};

export default LoginPage;
