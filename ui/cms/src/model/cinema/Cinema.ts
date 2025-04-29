import { EntityStatus } from "../common/EntityStatus";
import { RoomSummaryShort } from "../room/Room";

export interface Cinema {
  id: string;
  name: string;
  location: string;
  description: string;
  capacity: number;
  imageUrl?: string;
  status: EntityStatus;
  createdAt: Date;
  rooms: RoomSummaryShort[];
}

export interface CinemaSummary {
  id: string;
  name: string;
  location: string;
  description: string;
  capacity: number;
  imageUrl?: string;
  status: EntityStatus;
  createdAt: Date;
}

export interface CinemaSummaryShort {
  id: string;
  name: string;
  location: string;
  imageUrl?: string;
  createdAt: Date;
}

export interface CinemaCreateDto {
  name: string;
  location: string;
  description: string;
  capacity: number;
  imageUrl?: string | null | undefined;
}

export interface CinemaUpdateDto extends Partial<CinemaCreateDto> {
  status?: EntityStatus
}
