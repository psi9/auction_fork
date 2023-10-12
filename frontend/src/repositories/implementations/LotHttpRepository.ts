import { Lot } from "../../objects/Entities";
import ILotHttpRepository from "../interfaces/ILotHttpRepository";

export default class LotHttpRepository implements ILotHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async getAsync(): Promise<Lot[]> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/get_list`);
      if (!response.ok) throw new Error("Лоты не получены");

      const data = await response.json();

      return data;
    } catch (error) {
      throw new Error("Ошибка получения лотов, что-пошло не так");
    }
  }

  async getByAuctionAsync(auctionId: string): Promise<Lot[]> {
    try {
      const response = await fetch(
        `${this.baseURL}api/lot/get_list_by_auction/${auctionId}`
      );
      if (!response.ok) throw new Error("Лоты не получены");

      const data = await response.json();

      return data;
    } catch (error) {
      throw new Error("Ошибка получения лотов по аукциону, что-пошло не так");
    }
  }

  async postAsync(entity: Lot): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/create`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Лот не создан");
    } catch (error) {
      throw new Error("Ошибка создания лота, что-пошло не так");
    }
  }

  async putAsync(entity: Lot): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Лот не изменен");
    } catch (error) {
      throw new Error("Ошибка изменения лота, что-пошло не так");
    }
  }

  async deleteAsync(id: number): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/lot/delete/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) throw new Error("Лот не удален");
    } catch (error) {
      throw new Error("Ошибка удаления лота, что-пошло не так");
    }
  }
}
