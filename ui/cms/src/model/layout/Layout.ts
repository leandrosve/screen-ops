export enum LayoutElementType {
  STANDARD = 0,
  VIP = 1,
  DISABLED = 2, // Disabled meaning its not available
  ACCESSIBLE = 3,
  BLANK = 4,
  AISLE = 5,
}

export interface LayoutElement {
  id: string;
  label: string;
  index: number;
  type: LayoutElementType;
}

export interface Layout {
  id: string;
  name: string;
  rows: number;
  columns: number;
  createdAt: Date;
  elements: LayoutElement[];
  standardSeats: number;
  vipSeats: number;
  accesibleSeats: number;
  disabledSeats: number;
}

export interface LayoutSummary {
  id: string;
  name: string;
  rows: number;
  columns: number;
  createdAt: Date;
  standardSeats: number;
  vipSeats: number;
  accesibleSeats: number;
  disabledSeats: number;
}


export interface LayoutSearchFilters {
  name?: string;
}

export interface LayoutElementCreateDto {
  label: string;
  index: number;
  type: LayoutElementType;
}

export interface LayoutCreateDto {
  name: string;
  elements: LayoutElementCreateDto[];
  rows: number;
  columns: number;
}
