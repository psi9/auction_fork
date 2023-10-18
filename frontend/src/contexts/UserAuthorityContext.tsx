import { ReactNode, createContext, useContext, useState } from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";
import { useNavigate } from "react-router-dom";

interface IUserAuthorityContext {
  user: User | null;

  signup: (login: string, email: string, password: string) => void;
  signin: (email: string, password: string) => void;
  signout: () => void;

  checkAccess: () => boolean;
}

export const UserAuthorityContext = createContext<
  IUserAuthorityContext | undefined
>(undefined);

export const UserAuthorityProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  const userHttpRepository = new UserHttpRepository("https://localhost:7132/");
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);

  async function signup(login: string, email: string, password: string) {
    const user: User = {
      id: "5D9871B2-C2E4-4DAF-945F-A1E78F8724EC",
      name: login,
      email: email,
      password: password,
      token: "",
    };

    await userHttpRepository.postAsync(user);

    navigate("/");
  }

  async function signin(email: string, password: string) {
    const user = await userHttpRepository.signinAsync(email, password);

    if (!user) return;

    localStorage.setItem("authToken", user.token);
    localStorage.setItem("id", user.id);
    localStorage.setItem("username", user.name);
    localStorage.setItem("email", user.email);
    localStorage.setItem("password", user.password);

    navigate("/auctions");
    setUser(user);
  }

  function signout() {
    localStorage.clear();
    setUser(null);
    navigate("/authority");
  }

  function checkAccess(): boolean {
    if (!localStorage.getItem("authToken")) return false;

    const token = localStorage.getItem("authToken")!;
    const name = localStorage.getItem("username")!;
    const email = localStorage.getItem("email")!;
    const password = localStorage.getItem("password")!;
    const id = localStorage.getItem("id")!;

    const user: User = { id, name, email, password, token };
    setUser(user);

    return true;
  }

  return (
    <UserAuthorityContext.Provider
      value={{
        user,
        signup,
        signin,
        signout,
        checkAccess,
      }}
    >
      {children}
    </UserAuthorityContext.Provider>
  );
};

export const useUserAuthorityContext = () => useContext(UserAuthorityContext);
