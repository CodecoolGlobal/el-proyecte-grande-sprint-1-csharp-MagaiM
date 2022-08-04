import React from 'react';
import OtherNewsButtons from '../components/OtherNewsButtons';
import { Outlet } from "react-router-dom";

const News = () => {

    return (
        <div className="container">
            <div className='news-page-container'>
                <div className="news-container">
                    <Outlet />
                </div>
                <OtherNewsButtons />
            </div>
        </div>
    )
}

export default News