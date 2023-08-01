import "./Header.css"

export default function Header() {
    return (
        <header className="header">
        <a href="#default" className="logo">ЛоготипКомпании</a>
        <div className="header-right">
            <a className="active" href="#home">Главная</a>
            <a href="#contact">Контакты</a>
            <a href="#about">О нас</a>
        </div>
        </header>
    );
}