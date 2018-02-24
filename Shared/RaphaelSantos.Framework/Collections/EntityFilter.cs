using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RaphaelSantos.Framework.Collections
{
    /// <summary>
    /// Manipula dados de paginação e ordenação de uma coeção de entidades
    /// </summary>
    /// <typeparam name="EntityType">O Tipo da entidade</typeparam>
    public abstract class EntityFilter<EntityType> : IPageRequest, ISortRequest
        where EntityType : class
    {
        #region Constructors

        public EntityFilter()
        {
            SortMappings = new Dictionary<string, LambdaExpression>();
        }

        #endregion

        #region Properties

        private Dictionary<string, LambdaExpression> SortMappings { get; set; }

        /// <summary>
        /// Número da Página
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Tamanho da Página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Nome do Campo a ser ordenado
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Define se campo será ordenado de forma decrescente.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Define se a requisição é "light", ou seja, para metodos autocomplete
        /// </summary>
        public bool IsSimple { get; set; }

        /// <summary>
        /// Retorna a expressão referente á propriedade da entidade
        /// </summary>
        public LambdaExpression SortExpression
        {
            get
            {
                if (SortMappings == null || string.IsNullOrWhiteSpace(SortField) || !SortMappings.ContainsKey(SortField))
                    return null;

                return SortMappings[SortField];
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Mapeia um identificador de campo (nome) à uma propriedade (expressão)
        /// </summary>
        /// <param name="fieldKey">Identificador do campo</param>
        /// <param name="fieldExpression">Expressão da propriedade (LAMBDA)</param>
        public void MapSort(string fieldKey, Expression<Func<EntityType, object>> fieldExpression)
        {
            LambdaExpression lambda = fieldExpression;

            // Expressoes lambda com tipos diferente de string sao construidos com o Metodo "Convert"
            // Esse algoritmo remove essa chamada da expressao, deixando a chamada pura à propriedade
            // Exemplo: DE: i => Convert(i.Active) | PARA: i => i.Active
            var unary = fieldExpression.Body as UnaryExpression;
            if (unary != null)
                lambda = Expression.Lambda(unary.Operand, fieldExpression.Parameters);

            if (!SortMappings.ContainsKey(fieldKey))
                SortMappings.Add(fieldKey, lambda);
        }

        #endregion
    }
}
