import { useState } from "react";

import Button from "../../button/Button";

import "./AuthorityCard.css";

export default function AuthorityCard() {
  const [isSigned, setIsSigned] = useState<boolean>(true);

  return (
    <div className="authority_container">
      <div className="container_buttons">
        <button
          className={`type ${isSigned ? "active" : ""}`}
          onClick={() => setIsSigned(true)}
        >
          Авторизация
        </button>
        <button
          className={`type ${!isSigned ? "active" : ""}`}
          onClick={() => setIsSigned(false)}
        >
          Регистрация
        </button>
      </div>
      {isSigned ? (
        <div className="container_inputs">
          <input
            className="input_item"
            type="text"
            name="login"
            placeholder="Введите логин"
          ></input>
          <input
            className="input_item"
            type="email"
            name="email"
            placeholder="example@mail.ru"
          ></input>
        </div>
      ) : (
        <div className="container_inputs">
          <input
            className="input_item"
            type="text"
            name="login"
            placeholder="Введите логин"
          ></input>
          <input
            className="input_item"
            type="email"
            name="email"
            placeholder="example@mail.ru"
          ></input>
          <input
            className="input_item"
            type="password"
            name="password"
            placeholder="Введите пароль"
          ></input>
          <input
            className="input_item"
            type="password"
            name="repeat_password"
            placeholder="Повторите пароль"
          ></input>
        </div>
      )}

      <Button width="100%" text="Подтвердить" />
    </div>
  );
}
