import React from 'react'
import RecentNewsCard from './RecentNewsCard'

const RecentNews = ({newsData}) => {

    const formatDate = (timestamp) => {
        const x = new Date(timestamp);
        const dd = x.getDate();
        const mm = x.getMonth() + 1;
        const yy = x.getFullYear();
        return dd + " / " + mm + " / " + yy;
    }

    return (
        <div className="recent-news">
            <h3 className='recent-news-title'>Recent news</h3><br/>
            {newsData.map((news, index) => {
                let date = new Date(news.Date);
                let formattedDate = formatDate(date);
                return(
                <div key={index} className="recent-news-card" >
                    <RecentNewsCard news={news} formattedDate={formattedDate}/>
                    <br/>
                </div>
            )})}
        </div>
    )
}

export default RecentNews