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

export const UserProvider = ({ children }: { children: ReactNode }) => {
  const initialUsers: User[] = [
    {
      id: "",
      name: "",
      email: "",
      password: "",
      token: "",
    },
  ];

  const userRepository = new UserHttpRepository("https://localhost:7132/");
  const [users, setUsers] = useState<User[]>(initialUsers);

  useEffect(() => {
    async function fetchUsers() {
      setUsers(await userRepository.getAsync());
    }

    fetchUsers();
  });

  return <UserContext.Provider value={users}>{children}</UserContext.Provider>;
};

export const useUserContext = () => useContext(UserContext);
