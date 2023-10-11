import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AuthorityCard from "../../components/cards/authorityCard/AuthorityCard";
import { useUserAuthorityContext } from "../../contexts/UserAuthorityContext";

import "./AuthorityPage.css";

export default function AuthorityPage() {
  const userAuthorityContext = useUserAuthorityContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (userAuthorityContext?.checkAccess()) navigate("/auctions");
  });

  return (
    <div className="main">
      <AuthorityCard />
    </div>
  );
}
