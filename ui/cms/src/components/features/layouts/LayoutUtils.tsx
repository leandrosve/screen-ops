import { LayoutElementType } from "@/model/layout/Layout";
import { Icon } from "@chakra-ui/react";
import { ElementType } from "react";
import { FaAccessibleIcon, FaBorderNone, FaBan } from "react-icons/fa6";
import { MdEventSeat } from "react-icons/md";
import { RiVipCrown2Fill } from "react-icons/ri";
import { TbTriangleInvertedFilled } from "react-icons/tb";

interface LayoutIcon {
  icon: ElementType | null;
  boxSize?: string;
  color?: string;
}
const layoutIcons: Record<LayoutElementType, LayoutIcon> = {
  [LayoutElementType.ACCESSIBLE]: { icon: FaAccessibleIcon, boxSize: "1.3em" },
  [LayoutElementType.VIP]: { icon: RiVipCrown2Fill, boxSize: "1.25em" },
  [LayoutElementType.STANDARD]: {
    icon: MdEventSeat,
    boxSize: "1.3em",
  },
  [LayoutElementType.AISLE]: {
    icon: TbTriangleInvertedFilled,
    boxSize: "1.5em",
    color: "white/50",
  },
  [LayoutElementType.BLANK]: {
    icon: FaBorderNone,
    boxSize: "1.5em",
    color: "white",
  },
  [LayoutElementType.DISABLED]: { icon: FaBan, boxSize: "1.5em" },
};

const elementTypes = [
  {
    label: "Estandar",
    value: LayoutElementType.STANDARD,
    icon: layoutIcons[LayoutElementType.STANDARD],
  },
  {
    label: "VIP",
    value: LayoutElementType.VIP,
    icon: layoutIcons[LayoutElementType.VIP],
  },
  {
    label: "Accesible",
    value: LayoutElementType.ACCESSIBLE,
    icon: layoutIcons[LayoutElementType.ACCESSIBLE],
  },
  {
    label: "Deshabilitado",
    value: LayoutElementType.DISABLED,
    icon: layoutIcons[LayoutElementType.DISABLED],
  },
  {
    label: "Pasillo",
    value: LayoutElementType.AISLE,
    icon: layoutIcons[LayoutElementType.AISLE],
  },
  {
    label: "Vacío",
    value: LayoutElementType.BLANK,
    icon: layoutIcons[LayoutElementType.BLANK],
  },
];

const getIcon = (elementType: LayoutElementType, boxSize?: string): React.ReactElement | null => {
  const iconConfig = layoutIcons[elementType];

  if (!iconConfig || !iconConfig.icon) return null;

  return <Icon as={iconConfig.icon} boxSize={boxSize ?? iconConfig.boxSize} />;
};

const getBackgroundColor = (elementType: LayoutElementType) => {
  return [LayoutElementType.BLANK, LayoutElementType.AISLE].includes(
    elementType
  )
    ? "transparent"
    : "bg.inverted";
};

export {
  getIcon,
  getBackgroundColor,
  elementTypes,
  layoutIcons,
}