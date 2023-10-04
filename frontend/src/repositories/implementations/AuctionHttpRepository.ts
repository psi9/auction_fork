import { Auction } from "../../domain/Entities";
import IAuctionHttpRepository from "../interfaces/IAuctionHttpRepository";

export default class AuctionHttpRepository implements IAuctionHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Auction[]> {
    try {
      const response = await fetch(`${this.baseURL}/api/auction/get_list`);
      if (!response.ok) throw new Error("Аукционы не получены");

      const data = await response.json();

      return data;
    } catch (error) {
      throw new Error("Ошибка получения аукицонов, что-пошло не так");
    }
  }

  async postAsync(entity: Auction): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}/api/auction/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Аукцион не создан");
    } catch (error) {
      throw new Error("Ошибка создания аукицона, что-пошло не так");
    }
  }

  async putAsync(entity: Auction): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}/api/auction/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Аукцион не изменен");
    } catch (error) {
      throw new Error("Ошибка изменения аукицона, что-пошло не так");
    }
  }

  async deleteAsync(id: number): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}/api/auction/delete/${id}`, {
        method: "DELETE"
      });

      if (!response.ok) throw new Error("Аукцион не удален");
    } catch (error) {
      throw new Error("Ошибка удаления аукицона, что-пошло не так");
    }
  }
}
