import ApiService, { APIResponse } from "./ApiService";
import { Layout, LayoutCreateDto, LayoutSearchFilters, LayoutSummary } from "@/model/layout/Layout";

export default class LayoutService extends ApiService {
  protected static PATH = "/layouts";

  static async getLayouts(): Promise<APIResponse<LayoutSummary[]>> {
    return this.get("");
  }

  static async getDetail(id: string): Promise<APIResponse<Layout>> {
    return this.get(`/${id}`);
  }

  static async create(dto: LayoutCreateDto): Promise<APIResponse<Layout>> {
    return this.post("", dto);
  }

  static async update(
    id: string,
    dto: Partial<LayoutCreateDto>
  ): Promise<APIResponse<Layout>> {
    return this.patch(`/${id}`, dto);
  }
}
