import { createContext, useContext } from "react";

import { User } from "../domain/Entities";

export const UserContext = createContext<User[]>([]);

export const useUserContext = () => useContext(UserContext);
