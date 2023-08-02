import "./Header.css";

export default function Header() {
  const user = require("./assets/user.png");
  const search = require("./assets/search.png");
  const logo = require("./assets/logo.png");
  return (
    <header className="header">
      <div className="header_container">
        <div className="container_logo">
          <img className="logo" src={logo} alt="Логотип" />
          <div className="logo_text">Auctions</div>
        </div>
        <div className="container_content">
          <button className="content_item">Аукционы</button>
          <button className="content_item">Лоты</button>
          <button className="content_item">Участники</button>
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
