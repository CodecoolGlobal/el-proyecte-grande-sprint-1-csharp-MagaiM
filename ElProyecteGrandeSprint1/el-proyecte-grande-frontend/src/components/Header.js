import React from 'react';
import logo from '../Design/Imgs/KVMResized.jpg';
import AuthService from "../services/auth.service";
import { Link } from "react-router-dom";


const Header = ({currentUser, showModeratorBoard, showAdminBoard}) => {
    // const [cookies, setCookie, removeCookie] = useCookies(['user_id']);
    // const [userId, setUserId] = useState(localStorage.getItem("user_id"));
    // const [userName, setUserName] = useState(localStorage.getItem('user_name'));

    // useEffect(() => {
    //     if (cookies.user_id !== undefined && userId !== null) {
    //         if (cookies.user_id !== userId) {
    //             OnLogout();
    //         }
    //     }
    // }, [cookies, userId])

    // const OnLogout = () => {
    //     removeCookie("user_id");
    //     localStorage.clear();
    //}
    const logOut = () => {
        AuthService.logout();
      };
      
    return (
        <header>
            <nav className="navbar navbar-expand navbar-dark bg-black border-bottom box-shadow mb-3">
                <Link className="navbar-brand" type='button' to="/">
                    <img className="header-logo" src={logo} alt={'KWMGAMING'}/>
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
                    {currentUser ? (
                        <li className="nav-item">
                            <Link className="nav-link" type='button' to="/deals">
                                Deals
                            </Link>
                        </li>
                    ) : null}
                {/* {showModeratorBoard && (
                    <li className="nav-item">
                    <Link to={"/mod"} className="nav-link">
                        Moderator Board
                    </Link>
                    </li>
                )}
                {showAdminBoard && (
                    <li className="nav-item">
                    <Link to={"/admin"} className="nav-link">
                        Admin Board
                    </Link>
                    </li>
                )}
                {currentUser && (
                    <li className="nav-item">
                    <Link to={"/user"} className="nav-link">
                        User
                    </Link>
                    </li>
                )} */}
                </div>
                {currentUser ? (
                <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <Link to={"/profile"} className="nav-link">
                            {currentUser.UserName}
                        </Link>
                    </li>
                    {currentUser.Roles.includes("Admin") &&
                        <li className="nav-item">
                            <Link to={"/admin"} className="nav-link">
                                AdminBoard
                            </Link>
                        </li>}
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
            {/* <nav className="navbar-dark navbar navbar-expand-sm navbar-toggleable-sm bg-black border-bottom box-shadow mb-3">
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
            </nav> */}
        </header>
  )
}

export default Header