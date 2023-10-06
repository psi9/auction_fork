import { ReactNode, createContext, useContext, useState } from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";
import { useNavigate } from "react-router-dom";

interface UserAuthorityContextType {
  user: User | null;

  signup: (login: string, email: string, password: string) => void;
  signin: (email: string, password: string) => void;
  signout: () => void;

  checkAccess: () => boolean;
}

export const UserAuthorityContext = createContext<
  UserAuthorityContextType | undefined
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
      id: "",
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

    navigate("/auctions");
    setUser(user);
  }

  function signout() {
    navigate("/");
    setUser(null);
  }

  function checkAccess(): boolean {
    return user && user?.token ? true : false;
  }

  return (
    <UserAuthorityContext.Provider
      value={{ user, signup, signin, signout, checkAccess }}
    >
      {children}
    </UserAuthorityContext.Provider>
  );
};

export const useUserAuthorityContext = () => useContext(UserAuthorityContext);
