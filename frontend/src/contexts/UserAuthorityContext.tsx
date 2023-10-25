import { createContext, useEffect, useState, PropsWithChildren } from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";
import { useNavigate } from "react-router-dom";
import { sendSuccessNotice } from "../components/notification/Notification";

export interface IUserAuthorityContext {
  user: User | null;
  signup: (login: string, email: string, password: string) => void;
  signin: (email: string, password: string) => void;
  signout: () => void;
  reloadUserData: () => void;
}

export const UserAuthorityContext = createContext<IUserAuthorityContext>(
  {} as IUserAuthorityContext
);

export const UserAuthorityProvider: React.FC<PropsWithChildren> = ({
  children,
}) => {
  const userHttpRepository = new UserHttpRepository("https://localhost:7132/");
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    reloadUserData();
  }, []);

  async function signup(login: string, email: string, password: string) {
    const user: User = {
      id: "5D9871B2-C2E4-4DAF-945F-A1E78F8724EC",
      name: login,
      email: email,
      password: password,
    };

    if (!(await userHttpRepository.postAsync(user))) return;

    navigate("/authority");
  }

  async function signin(email: string, password: string) {
    const user = await userHttpRepository.signinAsync(email, password);

    if (!user) return;

    setUser(user);
    localStorage.setItem("id", user.id);

    navigate("/");

    sendSuccessNotice("Вы вошли успешно");
  }

  function signout() {
    localStorage.clear();
    setUser(null);
    navigate("/authority");
    sendSuccessNotice("Вы вышли успешно");
  }

  async function reloadUserData() {
    const id = localStorage.getItem("id");

    if (!id) {
      navigate("/authority");
      return;
    }

    const user = await userHttpRepository.getByIdAsync(id);

    if (!user) {
      navigate("/authority");
      return;
    }

    setUser(user);
  }

  return (
    <UserAuthorityContext.Provider
      value={{
        user: user,
        signup,
        signin,
        signout,
        reloadUserData,
      }}
    >
      {children}
    </UserAuthorityContext.Provider>
  );
};
