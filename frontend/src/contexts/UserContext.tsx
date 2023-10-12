import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";

import { User } from "../objects/Entities";

import UserHttpRepository from "../repositories/implementations/UserHttpRepository";

export const UserContext = createContext<User[]>([]);

const userRepository = new UserHttpRepository("https://localhost:7132/");

export const UserProvider = ({ children }: { children: ReactNode }) => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    async function fetchUsers() {
      setUsers(await userRepository.getAsync());
    }

    fetchUsers();
  }, []);

  return <UserContext.Provider value={users}>{children}</UserContext.Provider>;
};

export const useUserContext = () => useContext(UserContext);