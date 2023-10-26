import { createContext, useEffect, useState, PropsWithChildren } from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";
import { useLocation, useNavigate } from "react-router-dom";
import { enqueueSnackbar } from "notistack";

export interface IUserAuthorizationContext {
  user: User | null;
  members: User[] | null;

  signup: (login: string, email: string, password: string) => void;
  signin: (email: string, password: string) => void;
  signout: () => void;

  reloadUserData: () => void;
}

export const UserAuthorizationContext =
  createContext<IUserAuthorizationContext>({} as IUserAuthorizationContext);

export const UserAuthorizationProvider: React.FC<PropsWithChildren> = ({
  children,
}) => {
  const userHttpRepository = new UserHttpRepository("https://localhost:7132/");

  const navigate = useNavigate();
  const location = useLocation();

  const [user, setUser] = useState<User | null>(null);
  const [members, setMembers] = useState<User[] | null>(null);

  useEffect(() => {
    reloadUserData();
  }, []);

  useEffect(() => {
    async function fetchMembers() {
      if (!user) return;
      setMembers((await userHttpRepository.getAsync()).data!);
    }

    fetchMembers();
  }, [user]);

  async function signup(login: string, email: string, password: string) {
    const user: User = {
      id: "5D9871B2-C2E4-4DAF-945F-A1E78F8724EC",
      name: login,
      email: email,
      password: password,
    };

    if (!(await userHttpRepository.postAsync(user))) return;

    navigate("/authorization");
  }

  async function signin(email: string, password: string) {
    const user = await userHttpRepository.signinAsync(email, password);

    if (!user) return;

    setUser(user);
    localStorage.setItem("id", user.id);

    navigate("/");
  }

  function signout() {
    localStorage.clear();
    setUser(null);
    navigate("/authorization");
  }

  async function reloadUserData() {
    const id = localStorage.getItem("id");

    if (!id) {
      navigate("/authorization");
      return;
    }

    const user = await userHttpRepository.getByIdAsync(id);

    if (!user) {
      navigate("/authorization");
      return;
    }

    navigate(location);
    setUser(user!);
  }

  return (
    <UserAuthorizationContext.Provider
      value={{
        user,
        signup,
        signin,
        signout,
        reloadUserData,
        members,
      }}
    >
      {children}
    </UserAuthorizationContext.Provider>
  );
};
