import ApiService, { APIResponse } from "./ApiService";
import { Room, RoomCreateDto, RoomSearchFilters, RoomSummary, RoomUpdateDto } from "@/model/room/Room";

export default class RoomService extends ApiService {
  protected static PATH = "/rooms";

  static async getRooms(filters?: RoomSearchFilters): Promise<APIResponse<RoomSummary[]>> {
    return this.get("", filters ? this.buildQueryParams(filters) : "");
  }

  static async getDetail(id: string): Promise<APIResponse<Room>> {
    return this.get(`/${id}`);
  }

  static async create(dto: RoomCreateDto): Promise<APIResponse<Room>> {
    return this.post("", dto);
  }

  static async update(id: string, dto: RoomUpdateDto): Promise<APIResponse<Room>> {
    return this.patch(`/${id}`, dto);
  }
}
