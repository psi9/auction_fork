import {
  sendErrorNotice,
  sendWarnNotice,
} from "../../components/notification/Notification";
import { Result, User } from "../../objects/Entities";
import IUserHttpRepository from "../interfaces/IUserHttpRepository";

export default class UserHttpRepository implements IUserHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async signinAsync(
    email: string,
    password: string
  ): Promise<User | undefined> {
    try {
      const response = await fetch(`${this.baseURL}api/user/sign_in`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify({ email, password }),
        credentials: "include",
      });

      if (!response.ok) {
        sendWarnNotice("Вероятно, вы не зарегистрированы");
        return;
      }

      const data = await response.json();

      return data;
    } catch (error) {
      sendErrorNotice("Не удалось авторизоваться, попробуйте снова");
    }
  }

  async getAsync(): Promise<Result<User>> {
    try {
      const response = await fetch(`${this.baseURL}api/user/get_list`, {
        credentials: "include",
      });

      if (response.status === 401) {
        return { data: [], flag: false };
      }

      const data = await response.json();

      return { data, flag: false };
    } catch (error) {
      sendErrorNotice("Не удалось получить пользователей, попробуйте снова");
      return { data: [], flag: false };
    }
  }

  async getByIdAsync(id: string): Promise<User | undefined> {
    try {
      const response = await fetch(`${this.baseURL}api/user/get_by_id/${id}`, {
        credentials: "include",
      });

      if (response.status === 401) return;

      const data = await response.json();

      return data;
    } catch (error) {
      sendErrorNotice("Не удалось получить пользователей, попробуйте снова");
    }
  }

  async postAsync(entity: User): Promise<boolean> {
    try {
      await fetch(`${this.baseURL}api/user/sign_up`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
        credentials: "include",
      });

      return true;
    } catch (error) {
      sendErrorNotice("Не удалось зарегистрироваться, попробуйте снова");
      return false;
    }
  }

  async putAsync(entity: User): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/user/update`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json; charset: UTF-8;",
        },
        body: JSON.stringify(entity),
        credentials: "include",
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необхожимо зарегистрироваться");
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice(
        "Пользователь не был изменен, попробуйте снова, попробуйте снова"
      );
      return false;
    }
  }

  async deleteAsync(id: number): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/user/delete/${id}`, {
        method: "DELETE",
        credentials: "include",
      });

      if (response.status === 401) {
        sendWarnNotice("Вам необхожимо зарегистрироваться");
        return false;
      }

      return true;
    } catch (error) {
      sendErrorNotice(
        "Пользователь не был удален, попробуйте снова, попробуйте снова"
      );
      return false;
    }
  }
}
