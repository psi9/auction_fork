import { useState } from "react";

import "./AuthorityCard.css";
import { useUserAuthorityContext } from "../../../contexts/UserAuthorityContext";

export default function AuthorityCard() {
  const userAuthorityContext = useUserAuthorityContext();

  const [isSignin, setIsSignin] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const [login, setLogin] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [repeatPassword, setRepeatPassword] = useState<string>("");

  const loginValidation = (login: string): boolean => {
    if (!login) {
      setError("Поле логин должно быть заполнено!");
      return false;
    }

    // const expression = /^.*[~!@#$%^*\-_=+[{\]}\/;:,.?]{3}$/m;

    // if (!expression.test(login)) {
    //   setError("Логин не должен содержать специальных знаков!");
    //   return false;
    // }

    return true;
  };

  const emailValidation = (email: string): boolean => {
    if (!email) {
      setError("Поле почты должно быть заполнено!");
      return false;
    }

    const expression = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;

    if (!expression.test(email)) {
      setError("Введите корретную электронную почту!");
      return false;
    }

    return true;
  };

  const passwordValidation = (pwd: string): boolean => {
    if (!pwd) {
      setError("Поле пароля должно быть заполнено!");
      return false;
    }

    const expression = /^\S{8,30}$/m;

    if (!expression.test(pwd)) {
      setError("Пароль должен быть не короче 8 символов!");
      return false;
    }

    return true;
  };

  const repeatPasswordValidation = (pwd1: string, pwd2: string): boolean => {
    if (!pwd1 || !pwd2) {
      setError("Пароли должны быть указаны!");
      return false;
    }

    if (!(pwd1 === pwd2)) {
      setError("Пароли не совпадают!");
      return false;
    }

    return true;
  };

  const signin = () => {
    if (!emailValidation(email) || !passwordValidation(password)) return;

    userAuthorityContext?.signin(email, password);  
  };

  const signup = () => {
    if (!emailValidation(email) || !passwordValidation(password) || !
    repeatPasswordValidation(password, repeatPassword) || !loginValidation(login)) return;

    userAuthorityContext?.signup(login, email, password);  
  };

  return (
    <div className="authority_container">
      <div className="container_buttons">
        <button
          className={`type ${isSignin ? "active" : ""}`}
          onClick={() => setIsSignin(true)}
        >
          Авторизация
        </button>
        <button
          className={`type ${!isSignin ? "active" : ""}`}
          onClick={() => setIsSignin(false)}
        >
          Регистрация
        </button>
      </div>
      {isSignin ? (
        <div className="container_inputs">
          <input
            className="input_item"
            type="email"
            name="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="example@mail.ru"
          ></input>
          <input
            className="input_item"
            type="password"
            name="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            placeholder="Введите пароль"
          ></input>
          <button className="custom_button" onClick={() => signin()}>
            Подтвердить
          </button>
        </div>
      ) : (
        <div className="container_inputs">
          <input
            className="input_item"
            type="text"
            name="login"
            value={login}
            onChange={(event) => setLogin(event.target.value)}
            placeholder="Введите логин"
          ></input>
          <input
            className="input_item"
            type="email"
            name="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="example@mail.ru"
          ></input>
          <input
            className="input_item"
            type="password"
            name="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            placeholder="Введите пароль"
          ></input>
          <input
            className="input_item"
            type="password"
            name="repeat_password"
            value={repeatPassword}
            onChange={(event) => setRepeatPassword(event.target.value)}
            placeholder="Повторите пароль"
          ></input>
          <button className="custom_button" onClick={() => signup()}>
            Подтвердить
          </button>
        </div>
      )}
      <div className="error">{error}</div>
    </div>
  );
}
