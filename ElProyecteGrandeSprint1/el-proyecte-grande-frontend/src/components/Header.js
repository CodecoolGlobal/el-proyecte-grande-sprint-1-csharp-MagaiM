import React from 'react';
import logo from '../Design/Imgs/KVMResized.jpg';
import AuthService from "../services/auth.service";
import { Link } from "react-router-dom";


const Header = ({ currentUser }) => {

    const logOut = () => {
        AuthService.logout();
    };

    return (
        <>
            <header>
                <nav className="navbar navbar-expand navbar-dark bg-black box-shadow">
                    <Link className="navbar-brand" type='button' to="/">
                        <img className="header-logo" src={logo} alt={'KWMGAMING'} />
                    </Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-nav mr-auto">
                        <li className="nav-item">
                            <Link to={"/home"} className="nav-link">
                                Home
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" type='button' to="/news">
                                News
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" type='button' to="/articles">
                                Articles
                            </Link>
                        </li>
                        {currentUser ? (
                            <li className="nav-item">
                                <Link className="nav-link" type='button' to="/deals">
                                    Deals
                                </Link>
                            </li>

                        ) : null}
                        {currentUser && currentUser.Roles.includes("Admin") &&
                            <li className="nav-item">
                                <Link to={"/admin"} className="nav-link">
                                    AdminBoard
                                </Link>
                            </li>}
                    </div>
                    {currentUser ? (
                        <div className="navbar-nav ml-auto user-nav-item">
                            <li className="nav-item user-name">
                                <Link to={"/profile"} className="nav-link">
                                    {currentUser.UserName}
                                </Link>
                            </li>
                            <li className="nav-item">
                                <a href="/login" className="nav-link" onClick={logOut}>
                                    LogOut
                                </a>
                            </li>
                        </div>
                    ) : (
                        <div className="navbar-nav ml-auto">
                            <li className="nav-item">
                                <Link to={"/login"} className="nav-link">
                                    Login
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link to={"/register"} className="nav-link">
                                    Sign Up
                                </Link>
                            </li>
                        </div>
                    )}
                </nav>
            </header>
            <div className='divider' style={{ marginTop: "0px" }}></div>
        </>
    )
}

export default Header