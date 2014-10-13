package ar.com.utn.changuito.persistence;

import ar.com.utn.changuito.architecture.persistence.AbstractGAEDAO;
import ar.com.utn.changuito.architecture.persistence.ModelValidationException;
import ar.com.utn.changuito.model.Statistic;

public class StatisticDAO extends AbstractGAEDAO<Statistic> {

    public StatisticDAO() {
        super(Statistic.class);
    }

    @Override
    protected String getGAEEntityKind() {
        return "Statistic";
    }

    @Override
    protected long getId(final Statistic domainEntity) {
        return domainEntity.getId();
    }

    @Override
    protected void validateEntityModel(final Statistic entityModel) throws ModelValidationException {

        if (entityModel.getId() == 0)
            throw new ModelValidationException("Id cannot be zero");
    }
}