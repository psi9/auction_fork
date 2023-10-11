import { User } from "../../objects/Entities";
import IUserHttpRepository from "../interfaces/IUserHttpRepository";

export default class UserHttpRepository implements IUserHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async signinAsync(email: string, password: string): Promise<User> {
    try {
      const response = await fetch(
        `${this.baseURL}api/user/sign_in/${email}/${password}`
      );
      if (!response.ok) throw new Error("Пользователи не получены");

      const data = await response.json();

      return data;
    } catch (error) {
      throw new Error("Ошибка авторизации, что-пошло не так");
    }
  }

  async getAsync(): Promise<User[]> {
    try {
      const response = await fetch(`${this.baseURL}api/user/get_list`);
      if (!response.ok) throw new Error("Пользователи не получены");

      const data = await response.json();

      return data;
    } catch (error) {
      throw new Error("Ошибка получения пользователей, что-пошло не так");
    }
  }

  async postAsync(entity: User): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/user/sign_up`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Пользователь не создан");
    } catch (error) {
      throw new Error("Ошибка создания пользователя, что-пошло не так");
    }
  }

  async putAsync(entity: User): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/user/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
      });

      if (!response.ok) throw new Error("Пользователь не изменен");
    } catch (error) {
      throw new Error("Ошибка изменения пользователя, что-пошло не так");
    }
  }

  async deleteAsync(id: number): Promise<void> {
    try {
      const response = await fetch(`${this.baseURL}api/user/delete/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) throw new Error("Пользователь не удален");
    } catch (error) {
      throw new Error("Ошибка удаления пользователя, что-пошло не так");
    }
  }
}
