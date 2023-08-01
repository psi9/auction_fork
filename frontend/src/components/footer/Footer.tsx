import "./Footer.css"

export default function Footer() {
    return(
        <footer className="footer">
            <div className="footer_content">
                <div className="footer_content_items">
                    <div className="footer_item">
                        <div className="footer_item_icon">
                            <img src="email.svg"/>
                        </div>
                        <div className="footer_item_text">
                            <p>emailpochta@gmail.com</p>
                            <span>Написать нам</span>
                        </div>
                    </div>
                    <div className="footer_item">
                        <p>2019 OOO "Анна"</p>
                    </div>
                    <div className="footer_item">
                        <div className="footer_item_icon">
                            <img src="email.svg"/>
                        </div>
                        <div className="footer_item_text">
                            <p>emailpochta@gmail.com</p>
                            <span>Написать нам</span>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    );
}