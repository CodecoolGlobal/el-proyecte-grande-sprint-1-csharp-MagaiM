import React, { useEffect, useState } from 'react';
import OtherNewsCard from '../components/OtherNewsCard'
import { Link, useLocation } from "react-router-dom";
import PropTypes from 'prop-types';

const OtherNews = ({ fetchData }) => {
    const [searchedNewsData, setSearchedNewsData] = useState([]);
    const baseurl = 'https://el-proyecte-grande-kvm-gaming.herokuapp.com/';
    const search = useLocation().search;
    const searchName = new URLSearchParams(search).get('search');

    useEffect(() => {
        fetchData(`${baseurl}News/${searchName}`)
            .then(data => { setSearchedNewsData(data) })
    }, [searchName])

    const formatDate = (timestamp) => {
        const x = new Date(timestamp);
        const dd = x.getDate();
        const mm = x.getMonth() + 1;
        const yy = x.getFullYear();
        return dd + " / " + mm + " / " + yy;
    }
    return (
        <div className='game-news'>
            <button className="top-news-button btn-outline-light" type="button">
                <Link className="nav-link text-light" type='button' to="/news">
                    <span className="top-news-button-content">Recent News</span>
                </Link>
            </button>
            {searchedNewsData.map((item, index) => {
                let date = new Date(item.Date);
                let formattedDate = formatDate(date);
                return (
                    <div key={index} className="other-news-card">
                        <OtherNewsCard item={item} formattedDate={formattedDate} />
                        <br />
                    </div>
                )
            })}
        </div>)
};

OtherNews.propTypes = {
    fetchData: PropTypes.func
};

export default OtherNews;