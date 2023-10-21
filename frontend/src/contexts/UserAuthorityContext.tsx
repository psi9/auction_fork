import { ReactNode, createContext, useContext, useState } from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";
import { useNavigate } from "react-router-dom";

interface IUserAuthorityContext {
  user: User | null;

  signup: (login: string, email: string, password: string) => void;
  signin: (email: string, password: string) => void;
  signout: () => void;

  reloadUserData: () => void;
}

export const UserAuthorityContext = createContext<
  IUserAuthorityContext | undefined
>(undefined);

export const UserAuthorityProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  const userHttpRepository = new UserHttpRepository("http://localhost:7132/");
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);

  async function signup(login: string, email: string, password: string) {
    const user: User = {
      id: "5D9871B2-C2E4-4DAF-945F-A1E78F8724EC",
      name: login,
      email: email,
      password: password,
    };

    await userHttpRepository.postAsync(user);

    navigate("/");
  }

  async function signin(email: string, password: string) {
    const user = await userHttpRepository.signinAsync(email, password);

    if (!user) return;

    localStorage.setItem("id", user.id);
    localStorage.setItem("username", user.name);
    localStorage.setItem("email", user.email);

    navigate("/");
    setUser(user);
  }

  function signout() {
    localStorage.clear();
    setUser(null);
    navigate("/authority");
  }

  function reloadUserData() {
    const name = localStorage.getItem("username")!;
    const email = localStorage.getItem("email")!;
    const id = localStorage.getItem("id")!;

    if (!id) {
      navigate("/authority");
      return;
    }

    const user: User = { id, name, email, password: "" };
    setUser(user);
  }

  return (
    <UserAuthorityContext.Provider
      value={{
        user,
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

export const useUserAuthorityContext = () => useContext(UserAuthorityContext);
