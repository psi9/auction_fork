import { Link, useLocation } from "react-router-dom";

import "./Breadcrumbs.css";

export default function Breadcrumbs(props: { separator: string }) {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);

  return (
    <div className={`breadcrumb ${pathnames.length > 0 ? "" : "disactive"}`}>
      {pathnames.length > 0 && (
        <div className="breadcrumb_item">
          <Link className="link_breadcrumb" to="/">
            Главная
          </Link>
          <div className="breadcrumb_separator">{props.separator}</div>
        </div>
      )}
      {pathnames.map((name, index) => {
        const routeTo = `/${pathnames.slice(0, index + 1).join("/")}`;
        const isLast = index === pathnames.length - 1;

        return isLast ? (
          <div
            key={name}
            className="breadcrumb_item active"
            aria-current="page"
          >
            {name}
          </div>
        ) : (
          <div key={name} className="breadcrumb_item">
            <Link className="link_breadcrumb" to={routeTo}>
              {name}
            </Link>
            <div className="breadcrumb_separator">{props.separator}</div>
          </div>
        );
      })}
    </div>
  );
}
