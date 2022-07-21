import React, { useState, useEffect } from "react";
import { Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./Design/App.css";
import AuthService from "./services/auth.service";
import Login from "./routes/Login";
import Register from "./routes/Register";
import Home from "./routes/Home";
import Profile from "./routes/Profile";
import BoardUser from "./components/BoardUser";
import BoardModerator from "./components/BoardModerator";
import BoardAdmin from "./routes/BoardAdmin";
import ForgotPassword from "./routes/ForgotPassword";
import Header from './components/Header';
import News from './routes/News';
import Deals from './routes/Deals';
import RecentNews from './routes/RecentNews';
import OtherNews from './routes/OtherNews';


const App = () => {
  const [showModeratorBoard, setShowModeratorBoard] = useState(false);
  const [showAdminBoard, setShowAdminBoard] = useState(false);
  const [currentUser, setCurrentUser] = useState(undefined);


  useEffect(() => {
    if (currentUser) {
      setShowModeratorBoard(currentUser.Roles.includes("Moderator"));
      setShowAdminBoard(currentUser.Roles.includes("Admin"));
    }
  }, [currentUser]);

  return (
    <div className='bg-dark text-white app-container'>
      <Header currentUser={currentUser} showModeratorBoard={showModeratorBoard} showAdminBoard={showAdminBoard} />
      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<Home fetchData={fetchData} />} />
          <Route path="/home" element={<Home fetchData={fetchData} />} />
          <Route path="/news" element={<News />}>
            <Route path='' element={<RecentNews fetchData={fetchData}/>}/>
            <Route path='/news/other-news' element={<OtherNews fetchData={fetchData}/>}/>
          </Route>
          <Route path="deals" element={<Deals fetchData={fetchData}/>}/>
          <Route path="/login" element={<Login setCurrentUser={setCurrentUser}/>} />
          <Route path="/register" element={<Register/>} />
          <Route path="/profile" element={<Profile/>} />
          <Route path="/user" element={<BoardUser/>} />
          <Route path="/mod" element={<BoardModerator/>} />
          <Route path="/admin" element={<BoardAdmin/>} />
          <Route path="/forgotPassword" element={<ForgotPassword/>} />
          <Route path="*" element={
                          <main style={{padding: "1rem"}}>
                            <p>There's nothing here!</p>
                          </main>}/>
        </Routes>
      </div>
    </div>
  );
};

async function fetchData(url) {
  const response = await fetch(url);
  if (response.ok){
      const data = await response.json();
      return data;
  }
  throw response;
}

export default App;

// import React, {useState} from 'react';
// import Header from './components/Header';
// import { Outlet } from "react-router-dom";
// import { AppContext } from "./lib/contextLib";
// import "bootstrap/dist/css/bootstrap.min.css";

// function App() {
//     const [isAuthenticated, userHasAuthenticated] = useState(false);
//     function handleLogout() {
//   userHasAuthenticated(false);
// }
//     return (
//         <AppContext.Provider value={{ isAuthenticated, userHasAuthenticated }}>
//         <div className='bg-dark text-white'>
//             <Header handleLogout={handleLogout} isAuthenticated={isAuthenticated}/>
//             <Outlet />
//         </div>
//         </AppContext.Provider>
//     );
// }
// export default App;