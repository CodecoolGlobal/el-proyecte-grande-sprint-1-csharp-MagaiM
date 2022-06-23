import React, {useState} from 'react';
import Header from './components/Header';
import { Outlet } from "react-router-dom";
import { AppContext } from "./lib/contextLib";

function App() {
    const [isAuthenticated, userHasAuthenticated] = useState(false);
    function handleLogout() {
  userHasAuthenticated(false);
}
    return (
        <AppContext.Provider value={{ isAuthenticated, userHasAuthenticated }}>
        <div className='bg-dark text-white'>
            <Header handleLogout={handleLogout} isAuthenticated={isAuthenticated}/>
            <Outlet />
        </div>
        </AppContext.Provider>
    );
}
export default App;