import { EntityStatus } from "@/model/common/EntityStatus";
import { Badge, BadgeProps, Icon } from "@chakra-ui/react";
import { FaRegEyeSlash } from "react-icons/fa6";
import { LuArchive } from "react-icons/lu";
import { MdOutlinePublic } from "react-icons/md";
import { RiDraftLine } from "react-icons/ri";
import { TiDelete } from "react-icons/ti";

interface Props extends BadgeProps{
  status: EntityStatus;
  genre?: "femenine" | "masculine";
}

const options = {
  [EntityStatus.DRAFT]: {
    label: "BORRADOR",
    labelMasculine: "BORRADOR",
    color: "pink",
    icon: {icon: RiDraftLine}
  },
  [EntityStatus.PUBLISHED]: {
    label: "PUBLICADA",
    labelMasculine: "PUBLICADO",
    color: "green",
    icon: {icon: MdOutlinePublic}
  },
  [EntityStatus.HIDDEN]: {
    label: "OCULTA",
    labelMasculine: "OCULTO",
    color: "blue",
    icon: {icon: FaRegEyeSlash}
  },
  [EntityStatus.ARCHIVED]: {
    label: "ARCHIVADA",
    labelMasculine: "ARCHIVADO",
    icon: {icon: LuArchive},
    color: "orange",
  },
  [EntityStatus.DELETED]: {
    label: "ELIMINADA",
    labelMasculine: "ELIMINADO",
    icon: {icon: TiDelete},
    color: "red",
  },
};

const EntityStatusBadge = ({ status, genre, ...rest}: Props) => {
  return (
    <Badge borderRadius='md' fontWeight='bold' paddingX={3} paddingY={1} height='fit-content' variant="subtle" colorPalette={options[status]?.color} {...rest}>
      <Icon as={options[status].icon.icon} />
      {genre == "masculine"
        ? options[status]?.labelMasculine ?? options[status]?.label
        : options[status]?.label}
    </Badge>
  );
};

export default EntityStatusBadge;
