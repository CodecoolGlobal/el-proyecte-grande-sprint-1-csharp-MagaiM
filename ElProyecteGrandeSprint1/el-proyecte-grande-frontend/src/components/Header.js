import React from 'react'
import logo from '../Design/Imgs/KVMResized.jpg'
import { Link } from "react-router-dom";

const Header = () => {
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
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
  )
}

export default Header