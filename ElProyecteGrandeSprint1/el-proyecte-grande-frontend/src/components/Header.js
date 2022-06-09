import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types'
import { useState } from "react";

const Header = ({loadNews, loadDeals, loadHome}) => {
  return (
        <header>
            <nav className="navbar-dark navbar navbar-expand-sm navbar-toggleable-sm bg-black border-bottom box-shadow mb-3">
                <div className="container-fluid">
                    
                    <a className="navbar-brand" type='button' onClick={loadHome}><img className="" src="src/Design/Imgs/KVMResized.jpg" alt={'KWMGAMING'} style={{width: '100px', height: '40px'}}/></a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-dark navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <a className="nav-link text-light" type='button' onClick={loadNews}>News</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link text-light" type='button' onClick={loadDeals}>Deals</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
  )
}


Header.prototype = {
    loadNews: PropTypes.func,
    loadDeals: PropTypes.func,
    loadHome: PropTypes.func,
}

export default Header