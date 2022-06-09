import React from 'react'
import OtherNewsCard from './OtherNewsCard'
import PropTypes from 'prop-types'

const OtherNews = ({newsData, showLatestNews}) => {

    const formatDate = (timestamp) => {
        const x = new Date(timestamp);
        const dd = x.getDate();
        const mm = x.getMonth() + 1;
        const yy = x.getFullYear();
        return dd + "/" + mm + "/" + yy;
    }

    return (
        <div className='game-news'>
            <button className="top-news-button btn-outline-light" type="button" onClick={showLatestNews} style={{float: 'right', backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}}>
                <span className="" style={{backgroundColor: '#c9b18e', color: '#303030', marginBottom: '10px'}} >Latest News</span>
            </button>
            {newsData.map((item, index) => {
                let date = new Date(item.Date);
                let formattedDate = formatDate(date);
                return(
                <div key={index} className="news" style={{width: '100%'}}>
                    <OtherNewsCard item={item} formattedDate={formattedDate}/>
                    <br/>
                </div>
            )})}
        </div>)
}

OtherNews.prototype = {
    showLatestNews: PropTypes.func
}
export default OtherNews