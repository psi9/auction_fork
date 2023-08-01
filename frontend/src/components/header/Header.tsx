import "./Header.css"

export default function Header() {
    return (
        <header className="header">
            <div className="container">
                <div className="logo">
                    <img src="./assets/logo.svg" alt="Логотип" />
                </div>
                <div className="content">
                    <button>Аукционы</button>
                    <button>Лоты</button>
                    <button>Участники</button>
                </div>
                <div className="tools">
                    <img src="./assets/search.svg" alt="Поиск" />
                    <img src="./assets/user.svg" alt="Профиль" />
                </div>
            </div>
        </header>
    );
}