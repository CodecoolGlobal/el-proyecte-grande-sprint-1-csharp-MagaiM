import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types'
import { useState } from "react";

const News = ({fetchData, showGameNews}) => {
    const [NewsData, setNewsData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/News')
        .then(NewsData => {
            setNewsData(NewsData);
        })
    }, [])

    const newsPageConntent = 
    (<div className="container">
        <div style={{display: 'flex', flexDirection: 'row'}}>
            <div className="news-container" style={{width: '70%'}}>
                <div className="recent-news">
                    <h3 style={{color: '#c9b18e'}}>Latest news</h3><br/>
                    {NewsData.map((news, index) => (
                        <div key={index} className="col-lg-3 col-lg-3" style={{display: 'inline-block', width: '100%'}}>
                            <div className="card" style={{flexDirection: 'row', backgroundColor: '#3A373F'}}>
                                <img className="product-image" src={String(news.image)} style={{maxHeight: '300px',maxWidth: '50%'}}/>
                                <div className="card-body inner-card" style={{width: '50%'}}>
                                    <a href={String(news.link)} style={{textDecoration: 'none', color: '#c9b18e'}}><h5 className="card-title">{news.title}</h5></a>
                                    <p className="card-text">{news.description}</p>
                                    <p className="card-text">{news.date}</p>
                                </div>
                            </div>
                            <br/>
                        </div>
                    ))}
                </div>
            </div>
            <div className="other-news" style={{width: '30%', padding: '0 0 0 2rem', float: 'right'}}>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}} onClick={showGameNews}><h5>Overwatch</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>World of Warcraft</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Warframe</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Fortnite</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>League of Legends</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Minecraft</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Borderlands</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Skyrim</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Resident Evil</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Hollow Knight</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Mega Man</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Sonic the Hedgehog</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Pokemon</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Call of Duty</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Grand Theft Auto</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>The Sims</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Tetris</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Assassin's Creed</h5></button><br/>
                <button className="btn-outline-light" type="button" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}><h5>Final Fantasy</h5></button><br/>
            </div>
        </div>
    </div>
  )

  return newsPageConntent;
}

News.prototype = {
    fetchData: PropTypes.func,
    showGameNews: PropTypes.func
}

export default News