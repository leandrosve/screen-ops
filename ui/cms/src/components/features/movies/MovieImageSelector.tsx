import {
  Box,
  Button,
  Card,
  Field,
  Flex,
  Grid,
  Icon,
  Input,
  Image,
  Text,
  IconButton,
  Group,
  Badge,
} from "@chakra-ui/react";
import type React from "react";

import { useState } from "react";
import {
  FaCircleInfo,
  FaGripVertical,
  FaPlus,
  FaTrashCan,
} from "react-icons/fa6";

interface Props {
  onChange: (urls: string[]) => void;
  value: string[];
}

export default function MovieImageSelector({ onChange, value }: Props) {

  const [newUrl, setNewUrl] = useState("");
  const [error, setError] = useState("");
  const [draggedIndex, setDraggedIndex] = useState<number | null>(null);

  const isValidUrl = (url: string) => {
    try {
      new URL(url);
      return url.match(/\.(jpeg|jpg|gif|png|webp)$/) !== null;
    } catch (e) {
      return false;
    }
  };

  const addImageUrl = () => {
   
    if (!newUrl.trim() || !isValidUrl(newUrl)) {
      setError("Por favor, ingresa una URL válida");
      return;
    }

    setError("");

    onChange([...value, newUrl]);
    setNewUrl("");
  };

  const removeImageUrl = (index: number) => {
    const newImageUrls = [...value];
    newImageUrls.splice(index, 1);
    onChange(newImageUrls);
  };

  const handleDragStart = (index: number) => {
    setDraggedIndex(index);
  };

  const handleDragOver = (e: React.DragEvent, index: number) => {
    e.preventDefault();
    if (draggedIndex === null) return;

    if (draggedIndex !== index) {
      const newImageUrls = [...value];
      const draggedItem = newImageUrls[draggedIndex];

      // Remove the dragged item
      newImageUrls.splice(draggedIndex, 1);

      // Insert at the new position
      newImageUrls.splice(index, 0, draggedItem);

      onChange(newImageUrls);
      setDraggedIndex(index);
    }
  };

  const handleDragEnd = () => {
    setDraggedIndex(null);
  };

  return (
    <Flex direction="column" gap={4} width="full" maxWidth={600}>
      <Flex direction="column" gap={4}>
        <Field.Root invalid={!!error}>
          <Field.Label>
            Imágenes adicionales
            <Badge size="xs" variant="surface">
              Opcional
            </Badge>
          </Field.Label>

          <Group attached width="full">
            <Input
              id="imageUrl"
              type="text"
              placeholder="Ejemplo: https://example.com/image.jpg"
              value={newUrl}
              onKeyDown={(e) => {
                if (e.key === "Enter") {
                  e.stopPropagation();

                  e.preventDefault();
                  addImageUrl();
                }
              }}
              onChange={(e) => setNewUrl(e.target.value)}
            />
            <Button onClick={addImageUrl} alignItems="center" display="flex">
              <Icon as={FaPlus} boxSize={4} mr={2} />
              Agregar imágen
            </Button>
          </Group>
          <Field.ErrorText>{error}</Field.ErrorText>
        </Field.Root>
      </Flex>

      {value.length > 0 ? (
        <Box spaceY={4}>
          <Text color="text.subtle" fontSize="sm">
            <Icon
              as={FaCircleInfo}
              boxSize={4}
              marginRight={1}
              marginTop="-0.2em"
            />{" "}
            Arrastra para reordenar las imágenes
          </Text>

          <Grid gap={4}>
            {value.map((url, index) => (
              <Card.Root
                key={index}
                draggable
                onDragStart={() => handleDragStart(index)}
                onDragOver={(e) => handleDragOver(e, index)}
                onDragEnd={handleDragEnd}
                position="relative"
                style={draggedIndex === index ? { opacity: 50 } : undefined}
              >
                <Card.Body
                  display="flex"
                  alignItems="center"
                  flexDirection="row"
                  padding={3}
                  gap={3}
                >
                  <div
                    className="cursor-move p-2 hover:bg-muted rounded-md"
                    title="Drag to reorder"
                  >
                    <Icon as={FaGripVertical} boxSize={5} fill="text.subtle" />
                  </div>

                  <Box
                    position="relative"
                    height={14}
                    width={14}
                    overflow="hidden"
                    rounded="md"
                    border="1px solid"
                    borderColor="borders.subtle"
                  >
                    <Image
                      src={url || "/placeholder.svg"}
                      width="full"
                      height="full"
                      alt={`Image ${index + 1}`}
                      onError={(e) => {
                        (e.target as HTMLImageElement).src =
                          "/placeholder.svg?height=64&width=64";
                      }}
                    />
                  </Box>
                  <Text flex={1} truncate fontSize="sm">{url}</Text>

                  <IconButton
                    variant="ghost"
                    colorPalette="red"
                    onClick={() => removeImageUrl(index)}
                    title="Eliminar"
                    marginLeft="auto"
                  >
                    <Icon as={FaTrashCan} boxSize={4} />
                  </IconButton>
                </Card.Body>
              </Card.Root>
            ))}
          </Grid>
        </Box>
      ) : (
        <Box
          textAlign="center"
          padding={8}
          border="1px solid"
          borderColor="border.emphasized"
          borderStyle="dashed"
          color="text.subtle"
          rounded="lg"
        >
          <p>Aún no se han añadido imágenes.</p>
        </Box>
      )}
    </Flex>
  );
}
