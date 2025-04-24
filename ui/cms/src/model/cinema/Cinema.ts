import { EntityStatus } from "../common/EntityStatus";

export interface Cinema {
  id: string;
  name: string;
  location: string;
  description: string;
  capacity: number;
  imageUrl?: string;
  status: EntityStatus;
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
