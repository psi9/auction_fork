import { useEffect, useState } from "react";

import Button from "../button/Button";

import "./Header.css";

export default function Header() {
  const user = require("./assets/user.png");
  const search = require("./assets/search.png");
  const logo = require("./assets/logo.png");

  const [isHeaderFixed, setIsHeaderFixed] = useState(false);

  useEffect(() => {
    function handleScroll() {
      const scrollY = window.scrollY;
      const header = document.querySelector(".header");
      const headerHeight = header?.clientHeight || 0;

      if (scrollY >= headerHeight) {
        setIsHeaderFixed(true);
      } else {
        setIsHeaderFixed(false);
      }
    }

    window.addEventListener("scroll", handleScroll);

    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <header className={`header ${isHeaderFixed ? "fixed" : ""}`}>
      <div className="header_container">
        <div className="container_logo">
          <img className="logo" src={logo} alt="Логотип" />
          <div className="logo_text">Auctions</div>
        </div>
        <div className="container_content">
          <Button width="100px" text="Аукционы" />
          <Button width="100px" text="Лоты" />
          <Button width="100px" text="Участники" />
        </div>
        <div className="container_tools">
          <button className="tool_item">
            <img className="item_img" src={user} alt="Профиль" />
          </button>
          <button className="tool_item">
            <img className="item_img" src={search} alt="Поиск" />
          </button>
        </div>
      </div>
    </header>
  );
}
