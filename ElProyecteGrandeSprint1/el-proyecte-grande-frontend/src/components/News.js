import React from 'react'
import PropTypes from 'prop-types'
import RecentNews from './RecentNews'
import OtherNews from './OtherNews'
import OtherNewsButtons from './OtherNewsButtons'

const News = ({ fetchData, showGameNews, searchedNews, showLatestNews }) => {

    return searchedNews == "" ? (
    <div className="container">
        <div className='news-page-container'>
            <div className="news-container" style={{width: '70%'}}>
                <RecentNews newsData={fetchData}/>
            </div>
            <OtherNewsButtons showGameNews={showGameNews} />
        </div>
    </div>
  ) : (
    <div className="container">
        <div className='news-page-container'>
            <div className="news-container" style={{width: '70%'}}>
                <OtherNews newsData={fetchData} showLatestNews={showLatestNews}/>
            </div>
            <OtherNewsButtons showGameNews={showGameNews}/>
        </div>
    </div>
  )
}

News.prototype = {
    showLatestNews: PropTypes.func,
    showGameNews: PropTypes.func,
    searchedNews: PropTypes.string
}

export default News