import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, FormLabel } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import jwtDecode from 'jwt-decode';
import { Form } from 'react-bootstrap';
import { useParams, useSearchParams } from 'react-router-dom';

const URL_GET_ALL_POLICIES = "https://localhost:7082/api/Policy/GetAll";
const URL_GET_ALL_COMPANIES = "https://localhost:7082/api/Company/GetAll";
const URL_GET_ALL_CATEGORIES = "https://localhost:7082/api/Category/GetAll";
const URL_BUY_INSURANCE = "https://localhost:7082/api/UserPolicy/Create";

const HomePage = () => {
    const [policies, setPolicies] = useState([]);
    const [companies, setCompanies] = useState([]);
    const [categories, setCategories] = useState([]);
    const { t, i18n } = useTranslation();
    const [searchParams, setSearchParams] = useSearchParams();
    const [searchString, setSearchString] = useState('');
    const [sortParameter, setSortParameter] = useState('');
    const [sortDirection, setSortDirection] = useState('');
    const [filterCompany, setFilterCompany] = useState('');
    const [filterCategory, setFilterCategory] = useState(searchParams.get("categoryId"));
    const [minPrice, setMinPrice] = useState('');
    const [maxPrice, setMaxPrice] = useState('');
    const [minCoverageAmount, setMinCoverageAmount] = useState('');
    const [maxCoverageAmount, setMaxCoverageAmount] = useState('');
    const [isFilteringMode, setIsFilteringMode] = useState(false);

    const handleResetFilters = async () => {
        resetFilters();
        await getAllPolicies();
    }

    const getAllPolicies = async () => {
        console.log(searchParams.get("categoryId"));

        try {
            const finalUrl = createUrl();
            console.log(finalUrl);
            const response = await axios.get(finalUrl);
            setPolicies(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching policies:', error);
        }
    };

    const createUrl = () => {
        let url = URL_GET_ALL_POLICIES;
        let isFirstSet = false;

        if (searchString != '') {
            if (isFirstSet) {
                url += '&searchString=' + searchString;
            }
            else {
                url += '?searchString=' + searchString;
                isFirstSet = true;
            }
        }

        if (sortParameter != '') {
            if (isFirstSet) {
                url += '&sortParameter=' + sortParameter;
            }
            else {
                url += '?sortParameter=' + sortParameter;
                isFirstSet = true;
            }
        }

        if (sortDirection != '') {
            if (isFirstSet) {
                url += '&sortDirection=' + sortDirection;
            }
            else {
                url += '?sortDirection=' + sortDirection;
                isFirstSet = true;
            }
        }

        if (filterCategory != '' && filterCategory != null) {
            if (isFirstSet) {
                url += '&categoryId=' + filterCategory;
            }
            else {
                url += '?categoryId=' + filterCategory;
                isFirstSet = true;
            }
        }

        if (filterCompany != '') {
            if (isFirstSet) {
                url += '&companyId=' + filterCompany;
            }
            else {
                url += '?companyId=' + filterCompany;
                isFirstSet = true;
            }
        }

        if (minCoverageAmount != '' || minCoverageAmount > 0) {
            if (isFirstSet) {
                url += '&minCoverageAmount=' + minCoverageAmount;
            }
            else {
                url += '?minCoverageAmount=' + minCoverageAmount;
                isFirstSet = true;
            }
        }

        if (maxCoverageAmount != '' || maxCoverageAmount > 0) {

            if (isFirstSet) {
                url += '&maxCoverageAmount=' + maxCoverageAmount;
            }
            else {
                url += '?maxCoverageAmount=' + maxCoverageAmount;
                isFirstSet = true;
            }
        }

        if (minPrice != '' || minPrice > 0) {
            if (isFirstSet) {
                url += '&minPrice=' + minPrice;
            }
            else {
                url += '?minPrice=' + minPrice;
                isFirstSet = true;
            }
        }

        if (maxPrice != '' || maxPrice > 0) {
            if (isFirstSet) {
                url += '&maxPrice=' + maxCoverageAmount;
            }
            else {
                url += '?maxPrice=' + maxCoverageAmount;
            }
        }

        return url;
    }

    const resetFilters = () => {
        setSearchString('');
        setSortParameter('');
        setSortDirection('');
        setFilterCategory('');
        setFilterCompany('');
        setMinPrice('');
        setMaxPrice('');
        setMinCoverageAmount('');
        setMaxCoverageAmount('');
    }

    const getAllCompanies = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_COMPANIES);
            setCompanies(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching companies:', error);
        }
    };

    const getAllCategories = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_CATEGORIES);
            setCategories(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    useEffect(() => {
        getAllPolicies();
        getAllCompanies();
        getAllCategories();
    }, []);

    const handleSortDirection = (e) => {
        setSortDirection(e.target.value);
    };

    const handleSortOption = (e) => {
        setSortParameter(e.target.value);
    };

    const handleFilterCompany = (e) => {
        setFilterCompany(e.target.value);
    };

    const handleFilterCategory = (e) => {
        setFilterCategory(e.target.value);
    };

    const handleMinPrice = (e) => {
        setMinPrice(e.target.value);
    };

    const handleMaxPrice = (e) => {
        setMaxPrice(e.target.value);
    };

    const handleMinCoverageAmount = (e) => {
        setMinCoverageAmount(e.target.value);
    };

    const handleMaxCoverageAmount = (e) => {
        setMaxCoverageAmount(e.target.value);
    };

    const handleSearch = (e) => {
        setSearchString(e.target.value);
    };

    const handleColapse = () => {
        setIsFilteringMode(false);
    }

    const handleOpenFilters = () => {
        if (!isFilteringMode) {
            setIsFilteringMode(true);
        }
        else {

            setIsFilteringMode(false);
        }
    }

    const handleBuyInsurance = async (buyPolicyId) => {
        try {
            console.log(buyPolicyId);
            const token = localStorage.getItem('token');
            const decodedToken = jwtDecode(token);
            console.log(decodedToken);
            const userId = decodedToken.UserId;
            console.log(userId);
            const startDate = new Date();
            const endDate = new Date();

            const requestBody = {
                startDate: startDate.toISOString(),
                endDate: endDate.toISOString(),
                userId: userId,
                policyId: buyPolicyId
            };
            const response = await axios.post(
                URL_BUY_INSURANCE,
                requestBody
            );
            console.log('Insurance policy purchased:', response.data);
        } 
        catch (error) {
            console.error('Error purchasing insurance policy:', error);
        }
    }

    return (
        <>
            <UserHeader />
            <div className="bg-info pb-4">
                <Container className='pt-3 bg-info'>
                    <h1 className="my-4">{t("homePage.insurancePoliciesHeader")}</h1>
                    <Card className='mb-3 bg-info p-3'>
                        <Button className="border border-2 border-light mb-2" onClick={() => { handleOpenFilters() }} variant='success'>{t('homePage.openFilters')}</Button>
                        <Button className="border border-2 border-light" onClick={() => { handleResetFilters() }} variant='danger'>{t('homePage.resetFilters')}</Button>
                    </Card>
                    {isFilteringMode && (
                        <Form className='bg-success p-3 border rounded border-danger border-2'>
                            <h3>{t("homePage.search")}</h3>
                            <div className="d-flex mt-3">
                                <Form.Label className='m-2 mt-0 mb-0'>
                                    <p>
                                        {t('homePage.searchLabel')}
                                    </p>
                                </Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('homePage.searchPlaceholder')}
                                    value={searchString}
                                    onChange={handleSearch}
                                />
                            </div>
                            <h3>{t("homePage.sort")}</h3>
                            <div className="d-flex justify-content-around mt-3">
                                <Form.Label className='m-2 mt-0 mb-0'>
                                    <p>
                                        {t('homePage.sortlabel')}
                                    </p>
                                </Form.Label>
                                <Form.Select
                                    className='m-1 mt-0 mb-0'
                                    value={`${sortParameter}`}
                                    onChange={handleSortOption}
                                >
                                    <option value="name">{t('homePage.sortByName')}</option>
                                    <option value="price">{t('homePage.sortByPrice')}</option>
                                    <option value="coverageAmount">{t('homePage.sortByCoverageAmount')}</option>
                                    
                                </Form.Select>
                                <Form.Select
                                    className='m-1 mt-0 mb-0'
                                    value={`${sortDirection}`}
                                    onChange={handleSortDirection}
                                >
                                    <option value="asc">{t('homePage.sortByAsc')}</option>
                                    <option value="desc">{t('homePage.sortBDesc')}</option>
                                    
                                </Form.Select>
                            </div>
                            <h3 className='mt-3'>{t("homePage.filtering")}</h3>
                            <Form.Group className='mt-3' controlId="filterCompany">
                                <Form.Label>{t('homePage.filterByCompany')}</Form.Label>
                                <Form.Select value={filterCompany} onChange={handleFilterCompany}>
                                    <option value="">{t('homePage.allCompanies')}</option>
                                    {companies?.map((company) => (
                                        <option key={company.id} value={company.id}>{company.companyName}</option>
                                    ))}
                                </Form.Select>
                            </Form.Group >
                            <Form.Group className='mt-3' controlId="filterCategory">
                                <Form.Label>{t('homePage.filterByCategory')}</Form.Label>
                                <Form.Select value={filterCategory} onChange={handleFilterCategory}>
                                    <option value="">{t('homePage.allCategories')}</option>
                                    {categories?.map((category) => (
                                        <option key={category.id} value={category.id}>{category.categoryName}</option>
                                    ))}
                                </Form.Select>
                            </Form.Group>

                            <Form.Group className='mt-3' controlId="Price">
                                <strong><p className='m-2 mt-0 mb-2'>{t('homePage.price')}</p></strong>
                                <div className="d-flex">
                                    <Form.Label className='m-2 mt-0 mb-0'>{t('homePage.min')}</Form.Label>
                                    <Form.Control type="number" min={0} value={minPrice} onChange={handleMinPrice} />
                                    <Form.Label className='m-2 mt-0 mb-0'>{t('homePage.max')}</Form.Label>
                                    <Form.Control type="number" min={0} value={maxPrice} onChange={handleMaxPrice} />
                                </div>
                            </Form.Group>

                            <Form.Group className='mt-3' controlId="CoverageAmount">
                                <strong><p className='m-2 mt-0 mb-2'>{t("homePage.coverageAmount")}</p></strong>
                                <div className="d-flex">
                                    <Form.Label className='m-2 mt-0 mb-0'>{t('homePage.min')}</Form.Label>
                                    <Form.Control type="number" min={0} value={minCoverageAmount} onChange={handleMinCoverageAmount} />
                                    <Form.Label className='m-2 mt-0 mb-0'>{t('homePage.max')}</Form.Label>
                                    <Form.Control type="number" min={0} value={maxCoverageAmount} onChange={handleMaxCoverageAmount} />
                                </div>
                            </Form.Group>

                            <Form.Group controlId="maxCoverageAmount">

                            </Form.Group>
                            
                            <div className=" mt-3 d-flex justify-content-around">
                                <Button className="p-5 pt-3 pb-3 m-1 mt-0 mb-0 border border-light border-2" variant="primary" onClick={getAllPolicies}>
                                    {t('homePage.apply')}
                                </Button>
                                <Button className="p-5 pt-3 pb-3 m-1 mt-0 mb-0 border border-light border-2" variant="secondary" onClick={handleColapse}>
                                    {t('homePage.colapse')}
                                </Button>
                            </div>
                        </Form>
                    )}
                    {policies.map((policy) => (
                        <Card key={policy.id} className="my-3 bg-warning border border-dark rounded border-2">
                            <Card.Body>
                                <Card.Title>{policy.name}</Card.Title>
                                <Card.Text>{policy.description}</Card.Text>
                                <Card.Text>
                                    <strong>{t("homePage.coverageAmount")}:</strong> {policy.coverageAmount}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t("homePage.price")}:</strong> {policy.price}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t("homePage.timePeriod")}:</strong> {policy.timePeriod}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t("homePage.category")}:</strong>{' '}
                                    <a href={`/Home?=${policy.category.id}`}>{policy.category.categoryName}</a>
                                </Card.Text>
                                <Card.Text>
                                    <strong>Company:</strong>{' '}
                                    <a href={`/companies/${policy.companyId}`}>{policy.company.companyName}</a>
                                </Card.Text>
                                <Button variant="primary" onClick={()=>{handleBuyInsurance(policy.id)}}>{t("homePage.buyNow")}</Button>
                            </Card.Body>
                        </Card>
                    ))}
                </Container>
            </div>
            <Footer />
        </>
    );
};

export default HomePage;