import React, { Component } from 'react';
import Button from 'react-bootstrap/lib/Button';
import Table from 'react-bootstrap/lib/Table';
import Panel from 'react-bootstrap/lib/Panel';
import ListGroup from 'react-bootstrap/lib/ListGroup';
import ListGroupItem from 'react-bootstrap/lib/ListGroupItem';

import DatePicker from 'react-datepicker';
import moment from 'moment';
import 'react-datepicker/dist/react-datepicker.css';
//import DayPickerInput from 'react-day-picker/DayPickerInput';
//import { DateUtils } from 'react-day-picker';
//import 'react-day-picker/lib/style.css';

export class GetItems extends Component {
        constructor(props) {
            super(props);

            this.state = {
                donnesDeSource: [],
                loading: false,
                premierDateCom: '',
                derniereDateCom: '',
                premierFactureDateCom: '',
                dernierFactureDateCom: '',
                nom: '',
                prenom: '',
                secteur: ''
            };
            this._handleChangeSCD = this._handleChangeSCD.bind(this);
            this._handleChangeECD = this._handleChangeECD.bind(this);
            this._handleChangeSFD = this._handleChangeSFD.bind(this);
            this._handleChangeEFD = this._handleChangeEFD.bind(this);
    }

    _handleChangeFN(value) {
        this.setState({ nom: value });
    }
    _handleChangeLN(value) {
        this.setState({ prenom: value });
    }
    _handleChangeSEC(value) {
        this.setState({ secteur: value });
    }
    _handleChangeSCD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                premierDateCom: ''
            }, () => console.log(this.state.premierDateCom));
        } else {
            this.setState({
                premierDateCom: value
            }, () => console.log(this.state.premierDateCom)); // This will show the updated state when state is set.
        }
    }
    _handleChangeECD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                derniereDateCom: ''
            }, () => console.log(this.state.derniereDateCom));
        } else {
            this.setState({
                derniereDateCom: value
            }, () => console.log(this.state.derniereDateCom)); // This will show the updated state when state is set.
        }
    }
    _handleChangeSFD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                premierFactureDateCom: ''
            }, () => console.log(this.state.premierFactureDateCom));
        } else {
            this.setState({
                premierFactureDateCom: value
            }, () => console.log(this.state.premierFactureDateCom)); // This will show the updated state when state is set.
        }
    }
    _handleChangeEFD(value) {
        if (typeof value === 'undefined') {
            this.setState({
                dernierFactureDateCom: ''
            }, () => console.log(this.state.dernierFactureDateCom));
        } else {
            this.setState({
                dernierFactureDateCom: value
            }, () => console.log(this.state.dernierFactureDateCom)); // This will show the updated state when state is set.
        }
    }

    AppelApiSelectionDonnees(e) {
        this.setState({
            loading: true
        });
        fetch(`api/DestinationFo/SelectionDonneesDITAsync?premierDateCom=${moment(this.state.premierDateCom).format('LLLL')}&derniereDateCom=${moment(this.state.derniereDateCom).format('LLLL')}&premierFactureDateCom=${moment(this.state.premierFactureDateCom).format('LLLL')}&dernierFactureDateCom=${moment(this.state.dernierFactureDateCom).format('LLLL')}&nom=${this.state.nom}&prenom=${this.state.prenom}&secteur=${this.state.secteur}`)
            .then(response => response.json())
            .then(data => {
                this.setState({ donnesDeSource: data, loading: false });
                console.log(data)
            });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Chargement...</em></p>
            : GetItems.renderDonneeTable(this.state.donnesDeSource);
        return (
            <div>
              
                <h1>Récupération des données</h1>
                <p>Récuperation des données à partir de la base de données.</p>
                <br />
                <form>
                    <Table striped bordered condensed hover>
                        <thead>
                            <tr>
                                <th>Première date Commande</th>
                                <th>Dernière date Commande </th>
                                <th>Première date Facture</th>
                                <th>Dernière date Facture</th>
                                <th>Nom</th>
                                <th>Prénom</th>
                                <th>Secteur</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td> <DatePicker                                    
                                    selected={this.state.premierDateCom}
                                    onChange={this._handleChangeSCD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..."/></td>
                                <td> <DatePicker
                                    selected={this.state.derniereDateCom}
                                    onChange={this._handleChangeECD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..."/></td>
                                <td> <DatePicker
                                    selected={this.state.premierFactureDateCom}
                                    onChange={this._handleChangeSFD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..."/></td>
                               <td>   <DatePicker
                                    selected={this.state.dernierFactureDateCom}
                                    onChange={this._handleChangeEFD}
                                    dateFormat="DD/MM/YYYY"
                                    placeholderText="Sélectionner date..." /></td>

                                <td>  <input type="text" value={this.state.nom} onChange={e => this._handleChangeFN(e.target.value)} name="_usr_first_name" /></td>
                                <td> <input type="text" value={this.state.prenom} onChange={e => this._handleChangeLN(e.target.value)} name="_usr_last_name" /></td>
                                <td> <input type="text" value={this.state.secteur} onChange={e => this._handleChangeSEC(e.target.value)} name="_usr_sect" /></td>
                            </tr>                           
                        </tbody>
                    </Table>
                    <div align="right">
                        <Button bsStyle="primary" bsSize="small" onClick={this.AppelApiSelectionDonnees.bind(this)} value="">Filtrer</Button>
                    </div>
                    <br />

                    {contents}
                    
                </form>
            </div>
        );
    }


    static renderDonneeTable(donnesDeSource) {
        return (
            <Table striped bordered condensed hover>
                <thead>
                    <tr>
                        <th>Colonne Id</th>
                        <th nowrap>Nom Utilsateur</th>
                        <th>Prénom Utilisateur</th>
                        <th>Secteur</th>
                        <th>Catalogue</th>
                        <th>Catalogue Autre</th>
                        <th>Version</th>
                        <th>Id Premier Com</th>
                        <th>Date Premier Com</th>
                        <th>Id Dernier Com</th>
                        <th>Date Dernier Com</th>
                        <th>Id Premier Fact</th>
                        <th>Date Premier Fact</th>
                        <th>Id Dernier Fact</th>
                        <th>Date Dernier Fact</th>
                        <th>Date Visite</th>
                        <th>Tables</th>
                        <th>Ligne</th>
                        <th>Table/Indexes</th>
                    </tr>
                </thead>
                <tbody>
                    {donnesDeSource.map(ds =>
                        <tr key={ds.colId}>
                            <td>{ds.colId}</td>
                            <td>{ds.sourceFormarteUtilNom}</td>
                            <td>{ds.sourceFormarteUtilPrenom}</td>
                            <td>{ds.sourceFormarteSectNom}</td>
                            <td>{ds.sourceFormarteCatalPrincipal}</td>
                            <td>{ds.sourceFormarteCatalAutrePrincipal}</td>
                            <td>{ds.sourceFormarteVersNom}</td>
                            <td>{ds.SourceFormarteComPremierDateId}</td>
                            <td>{moment(ds.sourceFormarteComPremierDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComDernierDateId}</td>
                            <td>{moment(ds.sourceFormarteComDerniereDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComPremierFactureDateId}</td>
                            <td>{moment(ds.sourceFormarteComPremierFactureDate).format('DD/MM/YYYY')}</td>
                            <td>{ds.sourceFormarteComDernierFactureDateId}</td>
                            <td>{moment(ds.sourceFormarteComDerniereFactureDate).format('DD/MM/YYYY')}</td>
                            <td>{moment(ds.sourceFormarteVisDate).format('DD/MM/YYYY')}</td>
                            <td>
                                <ListGroup>
                                    {
                                        ds.sourceFormarteTabNom.map((nt) =>
                                            <ListGroupItem bsStyle="success"> {nt} </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                            <td>
                                <ListGroup>
                                    {
                                        ds.sourceFormarteTabLigneComptage.map((ligne) =>
                                            <ListGroupItem bsStyle="success"> {ligne} </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                            <td>
                                <ListGroup bsStyle="success">
                                    {
                                        ds.sourceFormarteTabNomIndex.map((ligne) =>
                                            <ListGroupItem bsStyle="success">{ligne.tableNom} : {
                                                ligne.indexes.map((index) =>
                                                    <ListGroupItem bsStyle="info"> {index}  </ListGroupItem>
                                                )
                                            }
                                            </ListGroupItem>
                                        )
                                    }
                                </ListGroup>
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }
}