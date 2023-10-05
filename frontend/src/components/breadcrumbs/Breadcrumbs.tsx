import { Link, useLocation } from "react-router-dom";

export default function Breadcrumbs(props: { separator: string }) {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);

  return (
    <nav aria-label="breadcrumb">
      <ol className="breadcrumb">
        <li className="breadcrumb_item">
          <Link to="/auctions">Auctions</Link>
        </li>
        {pathnames.map((name, index) => {
          const routeTo = `/${pathnames.slice(0, index + 1).join("/")}`;
          const isLast = index === pathnames.length - 1;

          return isLast ? (
            <li
              key={name}
              className="breadcrumb_item active"
              aria-current="page"
            >
              {name}
            </li>
          ) : (
            <li key={name} className="breadcrumb_item">
              <Link to={routeTo}>{name}</Link>
              <span className="breadcrumb_separator">{props.separator}</span>
            </li>
          );
        })}
      </ol>
    </nav>
  );
}
