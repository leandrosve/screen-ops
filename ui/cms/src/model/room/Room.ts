import { Cinema, CinemaSummaryShort } from "../cinema/Cinema";
import { EntityStatus } from "../common/EntityStatus";
import { Layout } from "../layout/Layout";

export interface Room {
  id: string; 
  name: string;
  description: string;
  layout: Layout;
  status: EntityStatus;
  cinema: Cinema;
  publishedAt?: Date | null;
}

export interface RoomSummary {
  id: string;
  name: string;
  status: EntityStatus;
  description: string;
  publishedAt?: Date | null;
  cinema: CinemaSummaryShort;
}

export interface RoomSummaryShort {
  id: string;
  name: string;
  status: EntityStatus;
  description: string;
  publishedAt?: Date | null;
}

export interface RoomCreateDto {
  name: string;
  description: string;
  cinemaId: string;
  layoutId: string;

}

export interface RoomUpdateDto {
  name?: string;
  description?: string;
  layoutId?: string;
  status?: EntityStatus;
}

export interface RoomSearchFilters {
  cinemaId?: string | null;
  status?: number[] | null;
}
