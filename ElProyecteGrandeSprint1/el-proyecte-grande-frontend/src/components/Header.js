import React, { useEffect, useState } from 'react';
import logo from '../Design/Imgs/KVMResized.jpg';
import avatar from '../Design/Imgs/BaseProfilepicture.jpg';
import { Link } from "react-router-dom";
import { useCookies } from 'react-cookie';


const Header = ({}) => {
    const [cookies, setCookie, removeCookie] = useCookies(['user_id']);
    const [userId, setUserId] = useState(localStorage.getItem("user_id"));
    const [userName, setUserName] = useState(localStorage.getItem('user_name'));

    useEffect(() => {
        if (cookies.user_id !== undefined && userId !== null) {
            if (cookies.user_id !== userId) {
                OnLogout();
            }
        }
    }, [cookies, userId])

    const OnLogout = () => {
        removeCookie("user_id");
        localStorage.clear();
    }

    return (
        <header>
            <nav className="navbar-dark navbar navbar-expand-sm navbar-toggleable-sm bg-black border-bottom box-shadow mb-3">
                <div className="container-fluid">
                    
                    <Link className="navbar-brand" type='button' to="/">
                        <img className="header-logo" src={logo} alt={'KWMGAMING'}/>
                    </Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-dark navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <Link className="nav-link text-light" type='button' to="/news">
                                    News
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link text-light" type='button' to="/deals">
                                    Deals
                                </Link>
                            </li>
                            { cookies.user_id ? 
                            (<>
                                <li className="nav-item">
                                    <span className="nav-link text-light" type='button' onClick={OnLogout}>Logout</span>
                                </li>
                            </>
                            ) :
                            (<>
                            <li className="nav-item">
                                <Link className="nav-link text-light" type='button' to="/Login">
                                    Login
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link text-light" type='button' to="/Register">
                                    Register
                                </Link>
                            </li>
                            </>)
                            }
                        </ul>
                        { cookies.user_id ? 
                        (<div className="nav-item user-card">
                            <img className='avatar' src={avatar} alt="Avatar"></img>
                            <span>{userName}</span>
                        </div>
                        ):
                        (<></>)}
                    </div>
                </div>
            </nav>
        </header>
  )
}

export default Header