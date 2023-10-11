import { Link, useLocation } from "react-router-dom";

import "./Breadcrumbs.css";

export default function Breadcrumbs(props: { separator: string }) {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);
  const moreThanOne = pathnames.length > 1 ? true : false;

  return (
    <div className={`breadcrumb ${moreThanOne ? "" : "disactive"}`}>
      {pathnames.map((name, index) => {
        const routeTo = `/${pathnames.slice(0, index + 1).join("/")}`;
        const isLast = index === pathnames.length - 1;

        return isLast ? (
          <div
            key={name}
            className="breadcrumb_item active"
            aria-current="page"
          >
            {name.toLocaleUpperCase()}
          </div>
        ) : (
          <div key={name} className="breadcrumb_item">
            <Link className="link_breadcrumb" to={routeTo}>
              {name.toLocaleUpperCase()}
            </Link>
            <div className="breadcrumb_separator">{props.separator}</div>
          </div>
        );
      })}
    </div>
  );
}
