import { ReactNode, createContext, useContext, useState } from "react";

export const UserContext = createContext({});

export const UserProvider = ({ children }: { children: ReactNode }) => {
  const initialUsers = [
    {
      id: "",
      name: "",
      email: "",
      password: "",
      token: "",
    },
  ];

  const [users, setUsers] = useState(initialUsers);

  return (
    <UserContext.Provider value={{ users, setUsers }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUserContext = () => useContext(UserContext);
