import { enqueueSnackbar } from "notistack";
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
        enqueueSnackbar("Вам необходимо зарегистрироваться", {
          variant: "warning",
        });
        return;
      }

      const data = await response.json();

      enqueueSnackbar("Вы успешно авторизовались", { variant: "success" });

      return data;
    } catch (error) {
      enqueueSnackbar("Не удалось авторизоваться, попробуйте снова", {
        variant: "error",
      });
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
      enqueueSnackbar("Не удалось получить пользователей, попробуйте снова", {
        variant: "error",
      });
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
      enqueueSnackbar("Не удалось получить пользователей, попробуйте снова", {
        variant: "error",
      });
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

      enqueueSnackbar("Вы успешно зарегистрировались, нужно авторизоваться", {
        variant: "success",
      });

      return true;
    } catch (error) {
      enqueueSnackbar("Не удалось зарегистрироваться, попробуйте снова", {
        variant: "error",
      });
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
        enqueueSnackbar("Вам необходимо зарегистрироваться", {
          variant: "warning",
        });
        return false;
      }

      enqueueSnackbar("Информация успешно обновлена", { variant: "success" });

      return true;
    } catch (error) {
      enqueueSnackbar("Пользователь не был изменен, попробуйте снова", {
        variant: "error",
      });
      return false;
    }
  }

  async deleteAsync(id: string): Promise<boolean> {
    try {
      const response = await fetch(`${this.baseURL}api/user/delete/${id}`, {
        method: "DELETE",
        credentials: "include",
      });

      if (response.status === 401) {
        enqueueSnackbar("Вам необходимо зарегистрироваться", {
          variant: "warning",
        });
        return false;
      }

      enqueueSnackbar("Профиль удален", { variant: "success" });

      return true;
    } catch (error) {
      enqueueSnackbar("Пользователь не был удален, попробуйте снова", {
        variant: "error",
      });
      return false;
    }
  }
}
