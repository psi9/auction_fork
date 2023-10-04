import { Lot } from "../../domain/Entities";
import ILotHttpRepository from "../interfaces/ILotHttpRepository";

export default class LotHttpRepository implements ILotHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }
  async getAsync(): Promise<Lot[]> {
    throw new Error("Метод не имплементирован");
  }

  async postAsync(entity: Lot): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}/api/lot/create`, {
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
      const response = await fetch(`${this.baseURL}/api/lot/update`, {
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
      const response = await fetch(`${this.baseURL}/api/lot/delete/${id}`, {
        method: "DELETE"
      });

      if (!response.ok) throw new Error("Лот не удален");
    } catch (error) {
      throw new Error("Ошибка удаления лота, что-пошло не так");
    }
  }
}
