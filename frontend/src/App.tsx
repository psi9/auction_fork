import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Header from "./components/header/Header";
import Footer from "./components/footer/Footer";
import Arrow from "./components/arrow/Arrow";

import AuctionsPage from "./pages/AuctionsPage/AuctionsPage";
import AuthorityPage from "./pages/AuthorityPage/AuthorityPage";
import LotsPage from "./pages/LotsPage/LotsPage";
import UsersPage from "./pages/UsersPage/UsersPage";

import "./App.css";

function App() {
  return (
    <div className="App">
      <Header />
      <Router>
        <Routes>
          <Route index element={<AuthorityPage />}></Route>
          <Route path="/auctions" element={<AuctionsPage />}></Route>
          <Route path="/lots" element={<LotsPage />}></Route>
          <Route path="/users" element={<UsersPage />}></Route>
        </Routes>
      </Router>
      <Arrow />
      <Footer />
    </div>
  );
}

export default App;
