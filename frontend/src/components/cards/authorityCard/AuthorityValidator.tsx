type ResultType = {
  message: string | null;
  flag: boolean;
};

class AuthorityValidator {
  private login: string;
  private email: string;
  private password: string;
  private repeatPassword: string;

  constructor(
    login: string,
    email: string,
    password: string,
    repeatPassword: string
  ) {
    this.login = login;
    this.email = email;
    this.password = password;
    this.repeatPassword = repeatPassword;
  }

  public signinValidation(): void {
    this.login = "";
  }

  public signupValidation(): void {
    this.login = "";
  }

  private loginValidation = (login: string): ResultType => {
    if (!login) {
      return { message: "Поле логин должно быть заполнено!", flag: false };
    }

    const expression = /^.*[~!@#$%^*\-_=+[{\]}\/;:,.?]{3}$/m;

    if (!expression.test(login)) {
      return {
        message: "Логин не должен содержать специальных знаков!",
        flag: false,
      };
    }

    return { message: null, flag: true };
  };

  private emailValidation = (email: string): ResultType => {
    if (!email) {
      return { message: "Поле почты должно быть заполнено!", flag: false };
    }

    const expression = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;

    if (!expression.test(email)) {
      return { message: "Введите корретную электронную почту!", flag: false };
    }

    return { message: null, flag: true };
  };

  private passwordValidation = (pwd: string): ResultType => {
    if (!pwd) {
      return { message: "Поле пароля должно быть заполнено!", flag: false };
    }

    const expression = /^\S{8,30}$/m;

    if (!expression.test(pwd)) {
      return {
        message: "Пароль должен быть не короче 8 символов!",
        flag: false,
      };
    }

    return { message: null, flag: true };
  };

  private repeatPasswordValidation = (
    pwd1: string,
    pwd2: string
  ): ResultType => {
    if (!pwd1 || !pwd2) {
      return { message: "Пароли должны быть указаны!", flag: false };
    }

    if (!(pwd1 === pwd2)) {
      return { message: "Пароли не совпадают!", flag: false };
    }

    return { message: null, flag: true };
  };
}

export default AuthorityValidator;
